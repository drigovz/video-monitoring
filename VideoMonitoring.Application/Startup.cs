using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using VideoMonitoring.Infra.CrossCutting.DependencyInjection;
using VideoMonitoring.Infra.CrossCutting.Mappings;

namespace VideoMonitoring.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");
            configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureRepository.ConfigureDependenciesRepository(services);
            ConfigureService.ConfigureDependenciesServices(services);

            var config = new MapperConfiguration(x =>
            {
                x.AddProfile(new DtoToModelProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Video Monitoring API Doc",
                    Description = "API implementada utilizando ASP.NET Core Web API version 3.1 \n" +
                                  "\nObservações importantes: \n" +
                                  "- Os vídeos devem ser enviados no formato de código Base64 na requisição HTTP \n" +
                                  "- Para iniciar a aplicação, você deve entrar no projeto Api.Application e rodar o comando \"dotnet restore\", para restaurar as dependências do projeto, depois rode o comando \"dotnet build\" para construir o projeto e por fim rode o comando \"dotnet run\" para iniciar a aplicação. \n" +
                                  "- Para que a aplicação funcione corretamente, modifique a string de conexão no arquivo VideoMonitoring.Infra.Data/Context/AppDbContext.cs e VideoMonitoring.Infra.CrossCutting/DependencyInjection/ConfigureRepository.cs apontando para a instância SQL Server do seu banco de dados local ou algum banco de dados na nuvem.\n",
                    TermsOfService = new Uri("https://cla.opensource.microsoft.com/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Rodrigo Vaz",
                        Email = "rodrigodp2014@gmail.com",
                        Url = new Uri("https://github.com/drigovz/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use about CLA Open Source License",
                        Url = new Uri("https://cla.opensource.microsoft.com/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Video Monitoring API Doc");
                c.RoutePrefix = "swagger/ui";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
