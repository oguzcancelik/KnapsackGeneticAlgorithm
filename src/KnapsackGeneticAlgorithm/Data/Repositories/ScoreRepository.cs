using System.Threading.Tasks;
using KnapsackGeneticAlgorithm.Data.Entities;
using KnapsackGeneticAlgorithm.Data.Repositories.Abstractions;
using KnapsackGeneticAlgorithm.Infrastructure.Configuration;
using KnapsackGeneticAlgorithm.Infrastructure.Constants;
using Microsoft.Extensions.Options;

namespace KnapsackGeneticAlgorithm.Data.Repositories
{
    public class ScoreRepository : Repository, IScoreRepository
    {
        public ScoreRepository(IOptions<DatabaseOptions> databaseOptions) : base(databaseOptions)
        {
        }

        public async Task UpsertAsync(Score score)
        {
            var previousScore = await QueryFirstOrDefaultAsync<Score>(QueryConstants.GetScoreByNameQuery, new { score.Name });

            if (previousScore == null)
            {
                await InsertAsync(score);
            }
            else if (score.Profit > previousScore.Profit)
            {
                score.Id = previousScore.Id;

                await UpdateAsync(score);
            }
        }
    }
}