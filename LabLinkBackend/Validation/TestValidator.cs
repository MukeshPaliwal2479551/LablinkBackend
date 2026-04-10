using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Validation
{
    public class CreateTestValidator : AbstractValidator<CreateTestDto>
    {
        public CreateTestValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Test code is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Test name is required");
            RuleFor(x => x.ContainerTypeId).GreaterThan(0).WithMessage("Container type must be a valid ID");
            RuleFor(x => x.MinNormalValue).GreaterThan(0).WithMessage("Minimum normal value must be greater than 0");
            RuleFor(x => x.MaxNormalValue).GreaterThan(0).WithMessage("Maximum normal value must be greater than 0")
            .GreaterThan(x => x.MinNormalValue).WithMessage("Maximum normal value must be greater than minimum normal value");
            RuleFor(x => x.TatTargetMinutes).GreaterThan(0).When(x => x.TatTargetMinutes.HasValue)
            .WithMessage("TAT target minutes must be greater than o if provided");
        }
    }
}