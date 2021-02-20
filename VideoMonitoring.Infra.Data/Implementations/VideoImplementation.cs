using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoMonitoring.Domain.Entities;
using VideoMonitoring.Domain.Interfaces.Repository;
using VideoMonitoring.Infra.Data.Context;
using VideoMonitoring.Infra.Data.Repository;

namespace VideoMonitoring.Infra.Data.Implementations
{
    public class VideoImplementation : BaseRepository<Video>, IVideoRepository
    {
        private readonly DbSet<Video> _dataset;

        public VideoImplementation(AppDbContext context)
            : base(context)
        {
            _dataset = context.Set<Video>();
        }

        public async Task<IEnumerable<Video>> GetAllServerVideosAsync(Guid id)
        {
            var videos = await _dataset.Where(x => x.ServerId.ToString().ToLower().Trim() == id.ToString().ToString().ToLower().Trim()).ToListAsync();
            return videos;
        }
    }
}
