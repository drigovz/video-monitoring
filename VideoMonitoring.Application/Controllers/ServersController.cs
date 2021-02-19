using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoMonitoring.Domain.DTOs.Servers;
using VideoMonitoring.Domain.Interfaces.Services.ServerService;

namespace VideoMonitoring.Application.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ServersController : ControllerBase
    {
        private readonly IServerService _service;

        public ServersController(IServerService serverService)
        {
            _service = serverService;
        }

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
    }
}
