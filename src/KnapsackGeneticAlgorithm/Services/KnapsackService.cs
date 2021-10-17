using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnapsackGeneticAlgorithm.Data.Entities;
using KnapsackGeneticAlgorithm.Data.Repositories.Abstractions;
using KnapsackGeneticAlgorithm.Infrastructure.Constants;
using KnapsackGeneticAlgorithm.Infrastructure.Mappings;
using KnapsackGeneticAlgorithm.Models.Requests;
using KnapsackGeneticAlgorithm.Models.Responses;
using KnapsackGeneticAlgorithm.Services.Abstractions;

namespace KnapsackGeneticAlgorithm.Services
{
    public class KnapsackService : IKnapsackService
    {
        private readonly IGeneticAlgorithmService _geneticAlgorithmService;
        private readonly IRepository _repository;
        private readonly List<List<int>> _profitRecord;

        public KnapsackService(IGeneticAlgorithmService geneticAlgorithmService, IRepository repository)
        {
            _geneticAlgorithmService = geneticAlgorithmService;
            _repository = repository;
            _profitRecord = new List<List<int>>();
        }

        public async Task<KnapsackResponse> ApplyGeneticAlgorithmAsync(KnapsackRequest request)
        {
            _geneticAlgorithmService.InitializePopulation(request);

            var fittest = new List<int>();

            for (var i = 0; ContinueProcess(); i++)
            {
                fittest = await _geneticAlgorithmService.EvolveAsync();

                _profitRecord.Add(fittest);

                Console.WriteLine($"{i} - {_geneticAlgorithmService.CalculateFitness(fittest)}");
            }

            var scoreName = $"Instance_{request.Items.Count}_{request.Capacity}";

            var score = new Score
            {
                Name = scoreName,
                Capacity = request.Capacity,
                Profit = _geneticAlgorithmService.CalculateFitness(fittest),
                Weight = _geneticAlgorithmService.CalculateWeight(fittest),
                Result = $"[{string.Join(", ", fittest ?? new List<int>())}]"
            };

            await UpdateOrInsertToDatabase(score);

            return score.ToResponse();
        }

        private async Task UpdateOrInsertToDatabase(Score currentScore)
        {
            var previousScore = await _repository.QueryFirstOrDefaultAsync<Score>(QueryConstants.GetScoreByNameQuery, new { currentScore.Name });

            if (previousScore == null)
            {
                await _repository.InsertAsync(currentScore);
            }
            else if (currentScore.Profit > previousScore.Profit)
            {
                currentScore.Id = previousScore.Id;

                await _repository.UpdateAsync(currentScore);
            }
        }

        private bool ContinueProcess()
        {
            return _profitRecord.Count < 50 || _profitRecord.TakeLast(50).Distinct().Count() > 1;
        }
    }
}