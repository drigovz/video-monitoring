using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VideoMonitoring.Domain.DTOs.Servers;
using VideoMonitoring.Domain.DTOs.Videos;
using VideoMonitoring.Domain.Interfaces.Services.ServerService;
using VideoMonitoring.Domain.Interfaces.Services.VideoServices;

namespace VideoMonitoring.Application.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ServersController : ControllerBase
    {
        private readonly IServerService _service;
        private readonly IVideoService _videoService;
        private readonly IWebHostEnvironment _environment;

        public ServersController(IServerService serverService, IVideoService videoService, IWebHostEnvironment environment)
        {
            _service = serverService;
            _videoService = videoService;
            _environment = environment;
        }

        /// <summary>
        /// Buscar todos os servidores cadastrados
        /// </summary>
        /// <returns>Retorna uma lista com todos os servidores cadastrados no banco de dados</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServerDTO>>> GetAsync()
        {
            try
            {
                var servers = await _service.GetAllServersAsync();
                return Ok(servers.ToList());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        /// <summary>
        /// Buscar informações de um servidor
        /// </summary>
        /// <param name="id">ID do servidor</param>
        /// <returns>Retorna um objeto com informações do servidor</returns>
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([BindRequired] Guid id)
        {
            try
            {
                var server = await _service.GetServerByIdAsync(id);
                if (server == null)
                    return NotFound($"Server with id {id} not found");
                else
                    return new ObjectResult(server);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        /// <summary>
        /// Insere um novo servidor na base de dados.
        /// </summary>
        /// <param name="server">Dados do servidor a ser inserido</param>
        /// <returns>Retorna informações do novo servidor inserido</returns>
        [HttpPost("/api/server")]
        public async Task<ActionResult> AddAsync([FromBody] ServerDTO server)
        {
            try
            {
                if (server == null)
                    return BadRequest($"Error when try to add a new server");

                var result = await _service.AddServerAsync(server);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error when try to add a new server: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui um servidor cadastrado
        /// </summary>
        /// <param name="id">ID do servidor a ser excluído</param>
        /// <returns>Mensagem de exclusão bem sucedida</returns>
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteAsync([BindRequired] Guid id)
        {
            try
            {
                var result = await _service.GetServerByIdAsync(id);
                if (result == null)
                    return NotFound($"Server with id {id} not found");

                await _service.DeleteServerAsync(id);

                return StatusCode(StatusCodes.Status200OK, "Server deleted succesfull");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to delete server");
            }
        }

        /// <summary>
        /// Adiciona um vídeo a um servidor
        /// </summary>
        /// <param name="videoDto">vídeo a ser inserido no servidor</param>
        /// <param name="id">ID do servidor</param>
        /// <returns>Mensagem de inclusão de vídeo bem sucedida</returns>
        [HttpPost("{id:Guid}/videos")]
        public async Task<ActionResult> AddVideoAsync([FromBody] VideoDTO videoDto, [BindRequired] Guid id)
        {
            try
            {
                byte[] videoBytes = Convert.FromBase64String(videoDto.File.Trim());
                string guid = Guid.NewGuid().ToString("n"),
                       directory = Directory.GetCurrentDirectory() + "/videos/",
                       path = "",
                       video = "",
                       guidFormated = string.Format("{0}-{1}-{2}", guid.Substring(0, 4), guid.Substring(5, 4), guid.Substring(8, 4));

                if (!Directory.Exists(_environment.WebRootPath + directory))
                    Directory.CreateDirectory(_environment.WebRootPath + directory);

                video = $"vid_{guidFormated}.mp4";
                path = $"{directory}{video}";

                using (var fs = System.IO.File.Create(_environment.WebRootPath + directory + video))
                {
                    using (var bw = new BinaryWriter(fs))
                    {
                        bw.Write(videoBytes);

                        videoDto.Size = (int)bw.BaseStream.Length;
                        videoDto.FileName = video;

                        bw.Close();
                    }
                }

                if (!String.IsNullOrEmpty(path))
                {
                    var videoResult = await _videoService.AddVideoAsync(videoDto);
                    return new ObjectResult(videoResult);
                }
                else
                    return BadRequest($"Error when try to add a new video on server");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error when try to add a new video on server: {ex.Message}");
            }
        }

        /// <summary>
        /// Recuperar detalhes de um vídeo
        /// </summary>
        /// <param name="videoId">ID do vídeo</param>
        /// <returns>Objeto contendo informações do vídeo</returns>
        [HttpGet("{id:Guid}/videos/{videoId:Guid}")]
        public async Task<IActionResult> GetVideoById([BindRequired] Guid videoId)
        {
            try
            {
                var video = await _videoService.GetVideoByIdAsync(videoId);
                if (video == null)
                    return NotFound($"Video with id {videoId} not found");
                else
                    return new ObjectResult(video);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        /// <summary>
        /// Lista de todos os vídeos de um servidor
        /// </summary>
        /// <param name="id">ID do servidor</param>
        /// <returns>Lista com todos os vídeos de um servidor</returns>
        [HttpGet("{id:Guid}/videos")]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetAllVideosOfServerAsync([BindRequired] Guid id)
        {
            try
            {
                var servers = await _videoService.GetAllServerVideosAsync(id);
                return Ok(servers.ToList());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        /// <summary>
        /// Download do conteúdo binário de um vídeo
        /// </summary>
        /// <param name="videoId">ID do vídeo</param>
        /// <returns>Arquivo físico para download do vídeo</returns>
        [HttpGet("{id:Guid}/videos/{videoId:Guid}/binary")]
        public async Task<FileResult> DownloadBinaryVideoAsync([BindRequired] Guid videoId)
        {
            try
            {
                string file = await _videoService.GetFileVideosAsync(videoId);
                string filePath = Directory.GetCurrentDirectory() + "\\videos\\" + file;

                return PhysicalFile(filePath, "video/mp4", file);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Excluir um vídeo
        /// </summary>
        /// <param name="videoId">ID do vídeo</param>
        /// <returns>Mensagem de exclusão de vídeo bem sucedida</returns>
        [HttpDelete("{id:Guid}/videos/{videoId:Guid}")]
        public async Task<ActionResult> DeleteVideoAsync([BindRequired] Guid videoId)
        {
            try
            {
                var result = await _videoService.GetVideoByIdAsync(videoId);
                if (result == null)
                    return NotFound($"Video with id {videoId} not found");

                string fileName = result.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    var fileToDelete = Path.Combine(Directory.GetCurrentDirectory() + "\\videos\\" + fileName);
                    if (System.IO.File.Exists(fileToDelete))
                        System.IO.File.Delete(fileToDelete);
                }

                await _videoService.DeleteVideoAsync(videoId);

                return StatusCode(StatusCodes.Status200OK, "Video deleted succesfull");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to delete video");
            }
        }
    }
}
