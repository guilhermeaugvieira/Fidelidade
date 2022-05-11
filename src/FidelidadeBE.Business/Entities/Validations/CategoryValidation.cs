using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class CategoryValidation : BaseEntityValidation<Category>
{
    public CategoryValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.Name)
            .Length(3, 30).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.Level)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.Level)
            .GreaterThan(0).WithMessage("The field {PropertyName} must be greather than {ComparisonValue}");
    }
}