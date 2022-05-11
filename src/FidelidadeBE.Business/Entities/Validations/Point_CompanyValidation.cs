using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class Point_CompanyValidation : BaseEntityValidation<Point_Company>
{
    public Point_CompanyValidation()
    {
        RuleFor(c => c.PointId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.PointId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.CompanyId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.CompanyId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.Point)
            .SetValidator(new PointValidation()!).When(c => c.Point != null);

        RuleFor(c => c.Company)
            .SetValidator(new CompanyValidation()!).When(c => c.Company != null);
    }
}