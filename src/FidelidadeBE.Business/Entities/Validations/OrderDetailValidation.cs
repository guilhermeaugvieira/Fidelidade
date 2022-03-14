using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class OrderDetailValidation : BaseEntityValidation<OrderDetail>
{
    public OrderDetailValidation()
    {
        RuleFor(c => c.Point_ProductId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.DeliveryStatus)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .Length(3, 30)
            .WithMessage("The filed {PropertyName} must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.Product)
            .SetValidator(new Point_ProductValidation()!);
    }
}