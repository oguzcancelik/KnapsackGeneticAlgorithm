using System.Data.SQLite;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using KnapsackGeneticAlgorithm.Data.Repositories.Abstractions;
using KnapsackGeneticAlgorithm.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KnapsackGeneticAlgorithm.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly string _connectionString;

        public Repository(IOptions<DatabaseOptions> databaseOptions)
        {
            _connectionString = databaseOptions.Value.ConnectionString;
        }

        public async Task InsertAsync<T>(T entity) where T : class
        {
            await using var connection = new SQLiteConnection(_connectionString);

            await connection.InsertAsync(entity);
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            await using var connection = new SQLiteConnection(_connectionString);

            await connection.UpdateAsync(entity);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null)
        {
            await using var connection = new SQLiteConnection(_connectionString);

            var result = await connection.QueryFirstOrDefaultAsync<T>(query, parameters);

            return result;
        }
    }
}