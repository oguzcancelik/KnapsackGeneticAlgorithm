using Microsoft.AspNetCore.Builder;

namespace KnapsackGeneticAlgorithm.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSwaggerWithUi(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", nameof(KnapsackGeneticAlgorithm));
            });
        }
    }
}