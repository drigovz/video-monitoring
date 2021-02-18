using Microsoft.Extensions.DependencyInjection;
using VideoMonitoring.Domain.Interfaces.Services.ServerService;
using VideoMonitoring.Service.Services;

namespace VideoMonitoring.Infra.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesServices(IServiceCollection services)
        {
            services.AddTransient<IServerService, ServerService>();
        }
    }
}
