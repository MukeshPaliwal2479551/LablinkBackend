using FluentValidation;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Validation
{
    public class InstrumentRefDtoValidator : AbstractValidator<InstrumentRefDto>
    {
        public InstrumentRefDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Instrument name is required.")
                .MaximumLength(100).WithMessage("Instrument name cannot exceed 100 characters.");

            RuleFor(x => x.Model)
                .MaximumLength(100).WithMessage("Model cannot exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Model));

            RuleFor(x => x.Section)
                .MaximumLength(100).WithMessage("Section cannot exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Section));

            RuleFor(x => x.InterfaceTypeId)
                .GreaterThan(0).WithMessage("InterfaceTypeId must be a positive number.")
                .When(x => x.InterfaceTypeId.HasValue);

        }
    }
}
