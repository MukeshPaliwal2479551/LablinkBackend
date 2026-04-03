using FluentValidation;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Validation
{
    public class PanelDtoValidator : AbstractValidator<PanelDto>
    {
        public PanelDtoValidator()
        {
            RuleFor(p => p.TestIds)
                .NotEmpty().WithMessage("At least one Test ID is required")
                .Must(ids => ids.Count >= 1).WithMessage("Panel must contain at least one test");

            RuleFor(p => p.PanelName)
                .MaximumLength(255).WithMessage("Panel Name cannot exceed 255 characters")
                .When(p => !string.IsNullOrEmpty(p.PanelName));

            When(p => !p.Id.HasValue, () => {
                RuleFor(p => p.PanelCode)
                    .NotEmpty().WithMessage("Panel Code is required")
                    .MaximumLength(50).WithMessage("Panel Code cannot exceed 50 characters");
                RuleFor(p => p.PanelName)
                    .NotEmpty().WithMessage("Panel Name is required");
            });

            When(p => p.Id.HasValue && p.Id > 0, () => {
                RuleFor(p => p.Id).GreaterThan(0).WithMessage("Valid Panel ID is required");
            });
        }
    }
}
