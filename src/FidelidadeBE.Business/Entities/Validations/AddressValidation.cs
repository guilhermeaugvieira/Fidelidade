using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class AddressValidation : BaseEntityValidation<Address>
{
    public AddressValidation()
    {
        RuleFor(c => c.State)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.State)
            .Length(2).WithMessage("The field {PropertyName} must have {ComparisonValue} characters");

        RuleFor(c => c.City)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.City)
            .Length(3, 30).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.District)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.District)
            .Length(3, 30).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

        RuleFor(c => c.CEP)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.CEP)
            .Length(9).WithMessage("The field {PropertyName} must have {ComparisonValue} characters");

        RuleFor(c => c.Street)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.Street)
            .Length(3, 30).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");
        
        RuleFor(c => c.Number)
            .GreaterThan(0).When(c => c.Number != null).WithMessage("The field {PropertyName} must be higher than {ComparisonValue}");
    }
}