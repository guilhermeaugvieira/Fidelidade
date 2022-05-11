using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class ProductValidation : BaseEntityValidation<Product>
{
    public ProductValidation()
    {
        RuleFor(c => c.CategoryId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.CategoryId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.Name)
            .Length(3, 30).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.Points)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.Points)
            .GreaterThan(0).WithMessage("The field value must be greater than {ComparisonValue}");

        RuleFor(c => c.Category)
            .SetValidator(new CategoryValidation()!).When( c=> c.Category != null);
    }
}