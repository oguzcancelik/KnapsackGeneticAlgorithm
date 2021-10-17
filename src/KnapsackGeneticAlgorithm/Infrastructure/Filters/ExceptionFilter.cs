using System.Net;
using KnapsackGeneticAlgorithm.Infrastructure.Constants;
using KnapsackGeneticAlgorithm.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KnapsackGeneticAlgorithm.Infrastructure.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new ErrorResponse
            {
                Message = ErrorConstants.ErrorOccured
            };

            context.Result = new ObjectResult(response);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;
        }
    }
}