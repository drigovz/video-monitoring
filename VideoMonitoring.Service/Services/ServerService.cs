using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoMonitoring.Domain.Entities;
using VideoMonitoring.Domain.Interfaces;
using VideoMonitoring.Domain.Interfaces.Services.ServerService;

namespace VideoMonitoring.Service.Services
{
    public class ServerService : IServerService
    {
        private readonly IRepository<Server> _repository;

        public ServerService(IRepository<Server> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Server>> GetAllServersAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<Server> GetServerByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Server> AddServerAsync(Server server)
        {
            return await _repository.AddAsync(server);
        }

        public async Task<Server> UpdateServerAsync(Server server)
        {
            return await _repository.UpdateAsync(server);
        }

        public async Task<bool> DeleteServerAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
