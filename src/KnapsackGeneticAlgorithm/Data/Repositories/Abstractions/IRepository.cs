using System.Threading.Tasks;

namespace KnapsackGeneticAlgorithm.Data.Repositories.Abstractions
{
    public interface IRepository
    {
        Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null);

        Task InsertAsync<T>(T entity) where T : class;

        Task UpdateAsync<T>(T entity) where T : class;
    }
}