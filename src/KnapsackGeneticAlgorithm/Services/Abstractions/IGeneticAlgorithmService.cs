using System.Collections.Generic;
using System.Threading.Tasks;
using KnapsackGeneticAlgorithm.Models.Requests;

namespace KnapsackGeneticAlgorithm.Services.Abstractions
{
    public interface IGeneticAlgorithmService
    {
        List<int> InitializePopulation(KnapsackRequest request);

        Task<List<int>> EvolveAsync();

        int CalculateFitness(List<int> x);

        int CalculateWeight(List<int> x);
    }
}