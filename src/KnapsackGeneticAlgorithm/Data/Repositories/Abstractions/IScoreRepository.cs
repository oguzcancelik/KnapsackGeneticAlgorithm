using System.Threading.Tasks;
using KnapsackGeneticAlgorithm.Data.Entities;

namespace KnapsackGeneticAlgorithm.Data.Repositories.Abstractions
{
    public interface IScoreRepository : IRepository
    {
        Task UpsertAsync(Score score);
    }
}