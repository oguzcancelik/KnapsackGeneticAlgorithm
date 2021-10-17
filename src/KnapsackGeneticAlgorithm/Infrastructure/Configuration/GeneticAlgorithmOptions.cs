namespace KnapsackGeneticAlgorithm.Infrastructure.Configuration
{
    public class GeneticAlgorithmOptions
    {
        public decimal ParentSelectionRate { get; set; }

        public decimal NonParentSelectionRate { get; set; }

        public decimal MutationRate { get; set; }
    }
}