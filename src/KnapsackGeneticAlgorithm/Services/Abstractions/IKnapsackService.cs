using System.Threading.Tasks;
using KnapsackGeneticAlgorithm.Models.Requests;
using KnapsackGeneticAlgorithm.Models.Responses;

namespace KnapsackGeneticAlgorithm.Services.Abstractions
{
    public interface IKnapsackService
    {
        Task<KnapsackResponse> ApplyGeneticAlgorithmAsync(KnapsackRequest request);
    }
}