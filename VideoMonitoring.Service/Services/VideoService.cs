using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoMonitoring.Domain.DTOs.Videos;
using VideoMonitoring.Domain.Entities;
using VideoMonitoring.Domain.Interfaces;
using VideoMonitoring.Domain.Interfaces.Services.VideoServices;

namespace VideoMonitoring.Service.Services
{
    public class VideoService : IVideoService
    {
        private readonly IRepository<Video> _repository;
        private readonly IMapper _mapper;

        public VideoService(IRepository<Video> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VideoDTO>> GetAllVideosAsync()
        {
            var videos = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<VideoDTO>>(videos);
        }

        public async Task<VideoDTO> GetVideoByIdAsync(Guid id)
        {
            var videos = await _repository.GetByIdAsync(id);
            return _mapper.Map<VideoDTO>(videos);
        }

        public async Task<VideoDTO> AddVideoAsync(VideoDTO videoDto)
        {
            var video = _mapper.Map<Video>(videoDto);
            var result = await _repository.AddAsync(video);
            return _mapper.Map<VideoDTO>(result);
        }

        public async Task<VideoDTO> UpdateVideoAsync(VideoDTO videoDto)
        {
            var video = _mapper.Map<Video>(videoDto);
            var result = await _repository.UpdateAsync(video);
            return _mapper.Map<VideoDTO>(result);
        }

        public async Task<bool> DeleteVideoAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
