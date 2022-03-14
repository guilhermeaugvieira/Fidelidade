using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public abstract class BaseEntityValidation<TEntity> : AbstractValidator<TEntity> where TEntity : BaseEntity
{
    protected BaseEntityValidation()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.CreatedAt)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .NotEqual(default(DateTime)).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.UpdatedAt)
            .NotEqual(default(DateTime)).WithMessage("The field {PropertyName} can't be a default value")
            .GreaterThan(c => c.CreatedAt)
            .WithMessage("The field {PropertyName} must be greater than {ComparisonValue}");
    }
}