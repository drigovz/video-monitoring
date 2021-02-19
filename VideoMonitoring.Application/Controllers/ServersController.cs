using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IServerService _serverService;

        public ServersController(IServerService serverService)
        {
            _serverService = serverService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServerDTO>>> GetAsync()
        {
            try
            {
                var servers = await _serverService.GetAllServersAsync();
                return Ok(servers.ToList());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] ServerDTO server)
        {
            try
            {
                if (server == null)
                    return BadRequest($"Error when try to add a new server");

                var result = await _serverService.AddServerAsync(server);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error when try to add a new server: {ex.Message}");
            }
        }
    }
}
