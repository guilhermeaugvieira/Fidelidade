using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class ClientValidation : BaseEntityValidation<Client>
{
    public ClientValidation()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.AddressId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.CPF)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .Length(11).WithMessage("The filed {PropertyName} must have {ComparisonValue} characters");

        RuleFor(c => c.User)
            .SetValidator(new UserValidation()!);

        RuleFor(c => c.Address)
            .SetValidator(new AddressValidation()!);
    }
}