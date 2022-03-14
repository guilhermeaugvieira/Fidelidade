using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class Category_SubCategoryValidation : BaseEntityValidation<Category_SubCategory>
{
    public Category_SubCategoryValidation()
    {
        RuleFor(c => c.ParentCategoryId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.SubCategoryId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled")
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.ParentCategory)
            .SetValidator(new CategoryValidation()!);

        RuleFor(c => c.SubCategory)
            .SetValidator(new CategoryValidation()!);
    }
}