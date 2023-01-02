using FluentValidation;

namespace pa5.Algorithm;

public class ConfigurationValidation : AbstractValidator<Configuration>
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

        RuleFor(x => x.Ro)
            .GreaterThanOrEqualTo(0)
            .LessThan(5)
            .WithMessage($"Ro must be in the range: from 0 to 5");

        RuleFor(x => x.Beta)
           .GreaterThanOrEqualTo(0)
           .LessThan(5)
           .WithMessage($"Beta must be in the range: from 0 to 5");

        RuleFor(x => x.Alpha)
           .GreaterThanOrEqualTo(0)
           .LessThan(5)
           .WithMessage($"Alpha must be in the range: from 0 to 5");
    }
}
