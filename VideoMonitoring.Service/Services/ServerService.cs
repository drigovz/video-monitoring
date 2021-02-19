using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoMonitoring.Domain.DTOs.Servers;
using VideoMonitoring.Domain.Entities;
using VideoMonitoring.Domain.Interfaces;
using VideoMonitoring.Domain.Interfaces.Services.ServerService;

namespace VideoMonitoring.Service.Services
{
    public class ServerService : IServerService
    {
        private readonly IRepository<Server> _repository;
        private readonly IMapper _mapper;

        public ServerService(IRepository<Server> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServerDTO>> GetAllServersAsync()
        {
            var servers = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<ServerDTO>>(servers);
        }

        public async Task<ServerDTO> GetServerByIdAsync(Guid id)
        {
            var server = await _repository.GetByIdAsync(id);
            return _mapper.Map<ServerDTO>(server);
        }

        public async Task<ServerDTO> AddServerAsync(ServerDTO serverDto)
        {
            var server = _mapper.Map<Server>(serverDto);
            var result = await _repository.AddAsync(server);
            return _mapper.Map<ServerDTO>(result);
        }

        public async Task<ServerDTO> UpdateServerAsync(ServerDTO serverDto)
        {
            var server = _mapper.Map<Server>(serverDto);
            var result = await _repository.UpdateAsync(server);
            return _mapper.Map<ServerDTO>(result);
        }

        public async Task<bool> DeleteServerAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
