using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnapsackGeneticAlgorithm.Infrastructure.Configuration;
using KnapsackGeneticAlgorithm.Models;
using KnapsackGeneticAlgorithm.Models.Requests;
using KnapsackGeneticAlgorithm.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace KnapsackGeneticAlgorithm.Services
{
    public class GeneticAlgorithmService : IGeneticAlgorithmService
    {
        private List<ItemModel> _items;
        private List<List<int>> _population;
        private int _capacity;
        private readonly Random _random;
        private decimal _parentSelectionRate;
        private decimal _nonParentSelectionRate;
        private decimal _mutationRate;

        public GeneticAlgorithmService(IOptions<GeneticAlgorithmOptions> geneticAlgorithmOptions)
        {
            _random = new Random();
            _parentSelectionRate = geneticAlgorithmOptions.Value.ParentSelectionRate;
            _nonParentSelectionRate = geneticAlgorithmOptions.Value.NonParentSelectionRate;
            _mutationRate = geneticAlgorithmOptions.Value.MutationRate;
        }

        public List<int> InitializePopulation(KnapsackRequest request)
        {
            _items = request.Items;
            _capacity = request.Capacity;

            var totalWeight = _items.Sum(x => x.Weight);
            var randomize = totalWeight / _capacity < 5;

            _population = _items
                .Select(_ => Enumerable.Range(0, _items.Count)
                    .Select(_ => randomize ? _random.Next(0, 2) : 0)
                    .ToList())
                .OrderByDescending(CalculateFitness)
                .ToList();

            return _population.First();
        }

        public async Task<List<int>> EvolveAsync()
        {
            var parentThreshold = Convert.ToInt32(_parentSelectionRate * _population.Count);

            var parents = _population.Take(parentThreshold).ToList();
            var nonParents = _population.Skip(parentThreshold).ToList();

            parents.Add(new List<int>(parents.First()));
            parents.AddRange(nonParents.Where(_ => _nonParentSelectionRate >= (decimal)_random.NextDouble()));

            foreach (var parent in parents.Skip(1))
            {
                if (_mutationRate >= (decimal)_random.NextDouble())
                {
                    await Task.Run(() => Mutate(parent));
                }
            }

            await Task.Run(() => Crossover(parents, _population.Count));

            await Task.Run(UpdateRates);

            _population= parents.OrderByDescending(CalculateFitness).ToList();

            return _population.First();
        }

        public int CalculateFitness(List<int> x)
        {
            var profit = 0;
            var weight = 0;

            for (var i = 0; i < x.Count; i++)
            {
                if (x[i] == 1)
                {
                    profit += _items[i].Profit;
                    weight += _items[i].Weight;
                }
            }

            return _capacity >= weight ? profit : 0;
        }

        public int CalculateWeight(List<int> x)
        {
            var weight = 0;

            for (var i = 0; i < x.Count; i++)
            {
                if (x[i] == 1)
                {
                    weight += _items[i].Weight;
                }
            }

            return weight;
        }

        private void Mutate(List<int> x)
        {
            var position = _random.Next(x.Count);

            x[position] = x[position] == 1 ? 0 : 1;

            for (var tryCount = 0; tryCount < 10 && CalculateFitness(x) == 0; tryCount++)
            {
                position = _random.Next(x.Count);

                x[position] = 0;
            }
        }

        private void Crossover(List<List<int>> nextGeneration, int populationLength)
        {
            while (nextGeneration.Count < populationLength)
            {
                var (mother, father) = SelectMotherAndFather(nextGeneration);

                var child = new List<int>();

                for (var i = 0; i < _items.Count; i++)
                {
                    child.Add(mother[i] == father[i] ? mother[i] : _random.Next(0, 2));
                }

                if (_mutationRate >= (decimal)_random.NextDouble())
                {
                    Mutate(child);
                }

                nextGeneration.Add(child);
            }
        }

        private Tuple<List<int>, List<int>> SelectMotherAndFather(IReadOnlyList<List<int>> nextGeneration)
        {
            var motherIndex = _random.Next(nextGeneration.Count);
            var fatherIndex = _random.Next(nextGeneration.Count);

            while (motherIndex == fatherIndex)
            {
                fatherIndex = _random.Next(nextGeneration.Count);
            }

            return new Tuple<List<int>, List<int>>(nextGeneration[motherIndex], nextGeneration[fatherIndex]);
        }

        private void UpdateRates()
        {
            if (_parentSelectionRate >= (decimal)0.25)
            {
                _parentSelectionRate *= (decimal)0.9;
            }

            if (_mutationRate >= (decimal)0.25)
            {
                _mutationRate *= (decimal)0.9;
            }

            if (_nonParentSelectionRate <= (decimal)0.25)
            {
                _nonParentSelectionRate *= (decimal)1.1;
            }
        }
    }
}