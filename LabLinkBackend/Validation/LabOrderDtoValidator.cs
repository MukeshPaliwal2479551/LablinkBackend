using FluentValidation;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Validation;

public class LabOrderDtoValidator : AbstractValidator<LabOrderDto>
{
    public LabOrderDtoValidator()
    {
        RuleFor(x => x.PatientId)
            .GreaterThan(0)
            .WithMessage("PatientId must be a valid patient.");

        RuleFor(x => x.Priority)
            .InclusiveBetween(1, 2)
            .WithMessage("Priority must be 1 (Routine) or 2 (Stat).");

        RuleFor(x => x.ClientId)
            .GreaterThan(0)
            .When(x => x.ClientId.HasValue)
            .WithMessage("ClientId must be a valid ClientAccount.");

        RuleFor(x => x.OrderId)
            .Equal(0)
            .WithMessage("OrderId must not be provided by the client.");

        RuleFor(x => x.OrderDate)
            .Must(d => d == default)
            .WithMessage("OrderDate must not be provided by the client.");


        RuleFor(x => x.IsActive)
            .NotNull()
            .WithMessage("IsActive flag is required.");
    }
}