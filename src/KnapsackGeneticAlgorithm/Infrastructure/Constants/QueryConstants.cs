namespace KnapsackGeneticAlgorithm.Infrastructure.Constants
{
    public static class QueryConstants
    {
        public const string GetScoreByNameQuery = "SELECT * FROM Score WHERE Name = @Name;";
    }
}