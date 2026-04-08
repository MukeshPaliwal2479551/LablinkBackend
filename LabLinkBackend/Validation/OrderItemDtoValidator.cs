using FluentValidation;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Validation;

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage("OrderId is required.");

        RuleFor(x => x)
            .Must(x => x.TestId.HasValue || x.PanelId.HasValue)
            .WithMessage("Either TestId or PanelId must be provided.");

        RuleFor(x => x.Department)
            .MaximumLength(100)
            .When(x => x.Department != null);
    }
}