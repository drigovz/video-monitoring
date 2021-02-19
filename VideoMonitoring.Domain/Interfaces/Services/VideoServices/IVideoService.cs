using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoMonitoring.Domain.DTOs.Videos;

namespace VideoMonitoring.Domain.Interfaces.Services.VideoServices
{
    public interface IVideoService
    {
        Task<IEnumerable<VideoDTO>> GetAllVideosAsync();
        Task<VideoDTO> GetVideoByIdAsync(Guid id);
        Task<VideoDTO> AddVideoAsync(VideoDTO entity);
        Task<VideoDTO> UpdateVideoAsync(VideoDTO entity);
        Task<bool> DeleteVideoAsync(Guid id);
    }
}
