using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public abstract class BaseEntityValidation<TEntity> : AbstractValidator<TEntity> where TEntity : BaseEntity
{
    protected BaseEntityValidation()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.CreatedAt)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.CreatedAt)
            .NotEqual(default(DateTime)).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.UpdatedAt)
            .NotEqual(default(DateTime)).When(c => c is {UpdatedAt: null}).WithMessage("The field {PropertyName} can't be a default value");
            
        RuleFor(c => c.UpdatedAt)
            .GreaterThan(c => c.CreatedAt).When(c => c is {UpdatedAt: null}).WithMessage("The field {PropertyName} must be greater than {ComparisonValue}");
    }
}