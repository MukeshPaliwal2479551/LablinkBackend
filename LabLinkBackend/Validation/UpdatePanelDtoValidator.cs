using FluentValidation;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Validation
{
    public class UpdatePanelDtoValidator : AbstractValidator<UpdatePanelDto>
    {
        public UpdatePanelDtoValidator()
        {
            RuleFor(p => p.PanelId)
                .GreaterThan(0).WithMessage("Valid Panel ID is required");

            RuleFor(p => p.PanelName)
                .MaximumLength(255).WithMessage("Panel Name cannot exceed 255 characters")
                .When(p => !string.IsNullOrEmpty(p.PanelName));

            RuleFor(p => p.TestIds)
                .NotEmpty().WithMessage("At least one Test ID is required")
                .Must(ids => ids.Count >= 1).WithMessage("Panel must contain at least one test");
        }
    }
}

