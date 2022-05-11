using FluentValidation;

namespace FidelidadeBE.Business.Entities.Validations;

public class Category_SubCategoryValidation : BaseEntityValidation<Category_SubCategory>
{
    public Category_SubCategoryValidation()
    {
        RuleFor(c => c.ParentCategoryId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.ParentCategoryId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.SubCategoryId)
            .NotEmpty().WithMessage("The field {PropertyName} must be filled");
            
        RuleFor(c => c.SubCategoryId)
            .NotEqual(Guid.Empty).WithMessage("The field {PropertyName} can't be a default value");

        RuleFor(c => c.ParentCategory)
            .SetValidator(new CategoryValidation()!).When(c => c.ParentCategory != null);

        RuleFor(c => c.SubCategory)
            .SetValidator(new CategoryValidation()!).When(c => c.SubCategory != null);
    }
}