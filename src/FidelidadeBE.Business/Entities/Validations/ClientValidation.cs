using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class ClientValidation : BaseEntityValidation<Client>
{
    public ClientValidation()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.UserId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.AddressId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.AddressId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.CPF)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.CPF)
            .Length(11).WithMessage("The field {PropertyName} must have {ComparisonValue} characters");

        RuleFor(c => c.User)
            .SetValidator(new UserValidation()!).When(c => c.User != null);

        RuleFor(c => c.Address)
            .SetValidator(new AddressValidation()!).When(c => c.Address != null);
    }
}