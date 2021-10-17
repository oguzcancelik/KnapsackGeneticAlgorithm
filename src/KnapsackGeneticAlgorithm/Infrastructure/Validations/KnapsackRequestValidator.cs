using System.Linq;
using FluentValidation;
using KnapsackGeneticAlgorithm.Infrastructure.Constants;
using KnapsackGeneticAlgorithm.Infrastructure.Extensions;
using KnapsackGeneticAlgorithm.Models.Requests;

namespace KnapsackGeneticAlgorithm.Infrastructure.Validations
{
    public class KnapsackRequestValidator : AbstractValidator<KnapsackRequest>
    {
        public KnapsackRequestValidator()
        {
            RuleFor(x => x.Capacity)
                .Must(x => x > 0)
                .WithMessage(x => ValidationConstants.InvalidParameter.Format(nameof(x.Capacity)));

            RuleFor(x => x.Items)
                .Must(x => x is { Count: > 0 } && x.All(y => y.Profit >= 0 && y.Weight >= 0))
                .WithMessage(x => ValidationConstants.InvalidParameter.Format(nameof(x.Items)));
        }
    }
}