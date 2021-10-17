using System.Linq;
using System.Threading.Tasks;
using KnapsackGeneticAlgorithm.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KnapsackGeneticAlgorithm.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Where(x => !string.IsNullOrEmpty(x.ErrorMessage))
                    .Select(x => x.ErrorMessage)
                    .ToList();

                var response = new ErrorResponse
                {
                    Message = string.Join(", ", errors)
                };

                context.Result = new BadRequestObjectResult(response);

                return;
            }

            await next();
        }
    }
}