using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class Point_CompanyValidation : BaseEntityValidation<Point_Company>
{
    public Point_CompanyValidation()
    {
        RuleFor(c => c.PointId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.CompanyId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.Point)
            .SetValidator(new PointValidation()!);

        RuleFor(c => c.Company)
            .SetValidator(new CompanyValidation()!);
    }
}