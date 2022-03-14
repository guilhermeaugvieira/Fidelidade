using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class PointValidation : BaseEntityValidation<Point>
{
    public PointValidation()
    {
        RuleFor(c => c.ClientId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.AssignedPoints)
            .NotEqual(0).WithMessage("The field {PropertyName} must be different of {ComparisonValue}");

        RuleFor(c => c.Client)
            .SetValidator(new ClientValidation()!);
    }
}