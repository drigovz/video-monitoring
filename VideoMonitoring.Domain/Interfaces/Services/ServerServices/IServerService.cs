using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoMonitoring.Domain.DTOs.Servers;

namespace VideoMonitoring.Domain.Interfaces.Services.ServerService
{
    public interface IServerService
    {
        Task<IEnumerable<ServerDTO>> GetAllServersAsync();
        Task<ServerDTO> GetServerByIdAsync(Guid id);
        Task<ServerDTO> AddServerAsync(ServerDTO entity);
        Task<ServerDTO> UpdateServerAsync(ServerDTO entity);
        Task<bool> DeleteServerAsync(Guid id);        
    }
}
