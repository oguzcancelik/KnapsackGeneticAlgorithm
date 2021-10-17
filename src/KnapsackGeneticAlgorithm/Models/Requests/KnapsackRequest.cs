using System.Collections.Generic;

namespace KnapsackGeneticAlgorithm.Models.Requests
{
    public class KnapsackRequest
    {
        public int Capacity { get; set; }

        public List<ItemModel> Items { get; set; }
    }
}