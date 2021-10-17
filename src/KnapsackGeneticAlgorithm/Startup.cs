using FluentValidation.AspNetCore;
using KnapsackGeneticAlgorithm.Infrastructure.Extensions;
using KnapsackGeneticAlgorithm.Infrastructure.Filters;
using KnapsackGeneticAlgorithm.Infrastructure.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KnapsackGeneticAlgorithm
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers(x =>
                {
                    x.Filters.Add<ExceptionFilter>();
                    x.Filters.Add<ValidationFilter>();
                })
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<BaseValidator>());

            services.Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);

            services.AddSwagger();

            services.AddOptions(_configuration);

            services.AddRepositories();

            services.AddServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(x => x.MapControllers());

            app.UseSwaggerWithUi();
        }
    }
}