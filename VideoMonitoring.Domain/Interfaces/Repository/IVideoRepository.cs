using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoMonitoring.Domain.Entities;

namespace VideoMonitoring.Domain.Interfaces.Repository
{
    public interface IVideoRepository : IRepository<Video>
    {
        Task<IEnumerable<Video>> GetAllServerVideosAsync(Guid id);
        Task<string> GetFileVideosAsync(Guid id);
    }
}
