namespace FidelidadeBE.Business.Entities;

public class Category_SubCategory : BaseEntity
{
    public Guid ParentCategoryId { get; private set; }
    public Guid SubCategoryId { get; private set; }
    public virtual Category? ParentCategory { get; private set; }
    public virtual Category? SubCategory { get; private set; }

    protected Category_SubCategory() { }

    public Category_SubCategory(Guid subCategoryId, Guid categoryId)
    {
        ParentCategoryId = categoryId;
        SubCategoryId = subCategoryId;
    }
}