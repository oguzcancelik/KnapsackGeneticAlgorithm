using KnapsackGeneticAlgorithm.Data.Repositories;
using KnapsackGeneticAlgorithm.Data.Repositories.Abstractions;
using KnapsackGeneticAlgorithm.Infrastructure.Configuration;
using KnapsackGeneticAlgorithm.Services;
using KnapsackGeneticAlgorithm.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace KnapsackGeneticAlgorithm.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var openApiInfo = new OpenApiInfo
                {
                    Version = "v1",
                    Title = nameof(KnapsackGeneticAlgorithm),
                    Description = nameof(KnapsackGeneticAlgorithm)
                };

                options.SwaggerDoc("v1", openApiInfo);
            });
        }

        public static void AddOptions(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<DatabaseOptions>(configuration.GetSection(nameof(DatabaseOptions)));
            services.Configure<GeneticAlgorithmOptions>(configuration.GetSection(nameof(GeneticAlgorithmOptions)));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IScoreRepository, ScoreRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IKnapsackService, KnapsackService>();
            services.AddScoped<IGeneticAlgorithmService, GeneticAlgorithmService>();
        }
    }
}