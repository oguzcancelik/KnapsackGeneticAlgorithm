using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using KnapsackGeneticAlgorithm.Models.Requests;
using KnapsackGeneticAlgorithm.Models.Responses;
using KnapsackGeneticAlgorithm.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace KnapsackGeneticAlgorithm.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("knapsack")]
    public class KnapsackController : ControllerBase
    {
        private readonly IKnapsackService _knapsackService;

        public KnapsackController(IKnapsackService knapsackService)
        {
            _knapsackService = knapsackService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(KnapsackResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] KnapsackRequest request)
        {
            var result = await _knapsackService.ApplyGeneticAlgorithmAsync(request);

            return Ok(result);
        }
    }
}