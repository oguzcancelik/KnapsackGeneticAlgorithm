using KnapsackGeneticAlgorithm.Data.Entities;
using KnapsackGeneticAlgorithm.Models.Responses;

namespace KnapsackGeneticAlgorithm.Infrastructure.Mappings
{
    public static class ScoreMapper
    {
        public static KnapsackResponse ToResponse(this Score score)
        {
            return new KnapsackResponse
            {
                Result = score.Result,
                Value = score.Profit,
                Weight = score.Weight
            };
        }
    }
}