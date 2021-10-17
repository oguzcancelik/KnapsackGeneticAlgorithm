using Dapper.Contrib.Extensions;

namespace KnapsackGeneticAlgorithm.Data.Entities
{
    [Table("main.Score")]
    public class Score
    {
        [Key] 
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public int Weight { get; set; }

        public int Profit { get; set; }

        public string Result { get; set; }
    }
}