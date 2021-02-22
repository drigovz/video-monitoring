using VideoMonitoring.Infra.Data.Seeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace VideoMonitoring.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DatabaseGenerator.Seed();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
