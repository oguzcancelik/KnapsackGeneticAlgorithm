using System.Threading.Tasks;

namespace KnapsackGeneticAlgorithm.Data.Repositories.Abstractions
{
    public interface IRepository
    {
        Task InsertAsync<T>(T entity) where T : class;

        Task UpdateAsync<T>(T entity) where T : class;

        Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null);
    }
}