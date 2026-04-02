using FluentValidation;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Validation
{
    public class CreatePanelDtoValidator : AbstractValidator<CreatePanelDto>
    {
        public CreatePanelDtoValidator()
        {
            RuleFor(p => p.PanelCode)
                .NotEmpty().WithMessage("Panel Code is required")
                .MaximumLength(50).WithMessage("Panel Code cannot exceed 50 characters");

            RuleFor(p => p.PanelName)
                .NotEmpty().WithMessage("Panel Name is required")
                .MaximumLength(255).WithMessage("Panel Name cannot exceed 255 characters");

            RuleFor(p => p.TestIds)
                .NotEmpty().WithMessage("At least one Test ID is required")
                .Must(ids => ids.Count >= 1).WithMessage("Panel must contain at least one test");
        }
    }
}
