using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoMonitoring.Domain.Entities;

namespace VideoMonitoring.Domain.Interfaces.Services.ServerService
{
    public interface IServerService
    {
        Task<IEnumerable<Server>> GetAllServersAsync();
        Task<Server> GetServerByIdAsync(Guid id);
        Task<Server> AddServerAsync(Server entity);
        Task<Server> UpdateServerAsync(Server entity);
        Task<bool> DeleteServerAsync(Guid id);
    }
}
