using FluentValidation;
using pa4.Algorithm.Settings;

namespace pa4;

public class ConfigurationValidation : AbstractValidator<AlgorithmConfiguration>
{
    public ConfigurationValidation()
    {
        RuleFor(x => x.AntsCount)
            .GreaterThan(0)
            .LessThan(50);

        RuleFor(x => x.PointsCount)
            .GreaterThan(0)
            .LessThan(200);

        RuleFor(x => x.P)
            .GreaterThanOrEqualTo(0)
            .LessThan(5);

        RuleFor(x => x.B)
           .GreaterThanOrEqualTo(0)
           .LessThan(5);

        RuleFor(x => x.A)
           .GreaterThanOrEqualTo(0)
           .LessThan(5);
    }
}