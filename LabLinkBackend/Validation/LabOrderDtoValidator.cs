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

        RuleFor(x => x.OrderedByUserId)
            .GreaterThan(0)
            .WithMessage("OrderedByUserId must be a valid user.");

        RuleFor(x => x.Priority)
            .InclusiveBetween(1, 2)
            .WithMessage("Priority must be 1 (Routine) or 2 (Stat).");

        RuleFor(x => x.ClientId)
            .Must(x => x == null || x > 0)
            .WithMessage("ClientId must be null or a valid ClientAccount.");

        RuleFor(x => x.IsActive)
            .NotNull()
            .WithMessage("IsActive flag is required.");
    }
}
