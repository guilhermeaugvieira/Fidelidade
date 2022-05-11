using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class UserValidation : BaseEntityValidation<User>
{
    public UserValidation()
    {
        RuleFor(c => c.IdentityId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.IdentityId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.Name)
            .Length(3, 50).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");
    }
}