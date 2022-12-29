using FluentValidation;
using pa4.Algorithm.Settings;

namespace pa4;

public class ConfigurationValidation : AbstractValidator<AlgorithmConfiguration>
{
    public ConfigurationValidation()
    {
        RuleFor(x => x.AntsCount)
            .GreaterThan(0)
            .LessThanOrEqualTo(50)
            .WithMessage($"Ants count must be in the range: from 0 to 50");

        RuleFor(x => x.PointsCount)
            .GreaterThan(0)
            .LessThanOrEqualTo(200)
            .WithMessage($"Points count must be in the range: from 0 to 200");

        RuleFor(x => x.P)
            .GreaterThanOrEqualTo(0)
            .LessThan(5)
            .WithMessage($"Ro must be in the range: from 0 to 5");

        RuleFor(x => x.B)
           .GreaterThanOrEqualTo(0)
           .LessThan(5)
           .WithMessage($"Beta must be in the range: from 0 to 5");

        RuleFor(x => x.A)
           .GreaterThanOrEqualTo(0)
           .LessThan(5)
           .WithMessage($"Alpha must be in the range: from 0 to 5");
    }
}