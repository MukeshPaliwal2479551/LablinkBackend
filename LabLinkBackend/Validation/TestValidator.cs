using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Validation
{
    public class TestValidator : AbstractValidator<TestDto>
    {
        public TestValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Test code is required");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Test name is required");
            RuleFor(x => x.ContainerTypeId)
                .GreaterThan(0).WithMessage("Container type must be a valid ID");
            RuleFor(x => x.MaxNormalValue)
                .GreaterThanOrEqualTo(x => x.MinNormalValue)
                .WithMessage("Maximum normal value must be greater than or equal to minimum normal value");
            RuleFor(x => x.MinNormalValue)
                .LessThanOrEqualTo(x => x.MaxNormalValue)
                .WithMessage("Minimum normal value must be less than or equal to maximum normal value");
            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive must be specified");
        }
    }
}