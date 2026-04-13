using FluentValidation;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Validation;

public class AccessionDtoValidator : AbstractValidator<AccessionDto>
{
    public AccessionDtoValidator()
    {
        RuleSet("Create", () =>
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0)
                .WithMessage("OrderId must be greater than 0.");

            RuleFor(x => x.Section)
                .MaximumLength(50);

            RuleFor(x => x.AccessionId)
                .Equal(0)
                .WithMessage("AccessionId must not be provided.");

            RuleFor(x => x.AccessionNumber)
                .Empty()
                .WithMessage("AccessionNumber must not be provided.");

            RuleFor(x => x.AccessionDate)
                .Must(d => d == null || d == default)
                .WithMessage("AccessionDate must not be provided.");
        });

        RuleSet("UpdateSection", () =>
        {
            RuleFor(x => x.Section)
                .NotEmpty()
                .WithMessage("Section is required.")
                .MaximumLength(50);
        });
    }
}