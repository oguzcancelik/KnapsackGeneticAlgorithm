namespace KnapsackGeneticAlgorithm.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string Format(this string source, params object[] values)
        {
            return values is { Length: > 0 }
                ? string.Format(source, values)
                : source;
        }
    }
}