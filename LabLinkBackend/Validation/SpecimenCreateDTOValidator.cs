using FluentValidation;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Validation
{
    public class SpecimenCreateDTOValidator : AbstractValidator<SpecimenCreateDTO>
    {
        public SpecimenCreateDTOValidator()
        {
            RuleFor(x => x.OrderID)
                .GreaterThan(0).WithMessage("OrderID is required and must be greater than 0");

            RuleFor(x => x.OrderItemId)
                .GreaterThan(0).WithMessage("OrderItemId is required and must be greater than 0");

            RuleFor(x => x.SpecimenTypeId)
                .NotNull().WithMessage("SpecimenTypeId is required");

            RuleFor(x => x.ContainerTypeId)
                .NotNull().WithMessage("ContainerTypeId is required");

            RuleFor(x => x.CollectedBy)
                .NotNull().WithMessage("CollectedBy is required");

            RuleFor(x => x.CollectedDate)
                .NotNull().WithMessage("CollectedDate is required");
        }
    }
}
