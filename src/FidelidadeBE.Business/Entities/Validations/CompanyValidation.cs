using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class CompanyValidation : BaseEntityValidation<Company>
{
    public CompanyValidation()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.UserId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.AddressId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.AddressId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.CNPJ)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.CNPJ)
            .Length(14).WithMessage("The field {PropertyName} must have {ComparisonValue} characters");

        RuleFor(c => c.User)
            .SetValidator(new UserValidation()!).When(c => c.User != null);

        RuleFor(c => c.Address)
            .SetValidator(new AddressValidation()!).When(c => c.Address != null);
    }
}