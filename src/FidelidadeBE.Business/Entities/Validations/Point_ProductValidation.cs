using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class Point_ProductValidation : BaseEntityValidation<Point_Product>
{
    public Point_ProductValidation()
    {
        RuleFor(c => c.PointId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.PointId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.ProductId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.Point)
            .SetValidator(new PointValidation()!).When(c => c.Point != null);

        RuleFor(c => c.Product)
            .SetValidator(new ProductValidation()!).When( c=> c.Product != null);
    }
}