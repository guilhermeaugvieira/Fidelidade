using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class ProductValidation : BaseEntityValidation<Product>
{
    public ProductValidation()
    {
        RuleFor(c => c.CategoryId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .Length(3, 30)
            .WithMessage("The filed {PropertyName} must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.Points)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .GreaterThan(0).WithMessage("The field value must be greater than {ComparisonValue}");

        RuleFor(c => c.Category)
            .SetValidator(new CategoryValidation()!);
    }
}