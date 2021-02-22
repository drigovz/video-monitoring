using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VideoMonitoring.Domain.Interfaces;
using VideoMonitoring.Domain.Interfaces.Repository;
using VideoMonitoring.Infra.Data.Context;
using VideoMonitoring.Infra.Data.Implementations;
using VideoMonitoring.Infra.Data.Repository;

namespace VideoMonitoring.Infra.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IVideoRepository, VideoImplementation>();

            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer("Data Source=[YOUR_SERVER]; initial catalog=VideoMonitoring; user id=[YOUR_DB_USER]; password=[YOUR_PASSWORD];")
            );
        }
    }
}
