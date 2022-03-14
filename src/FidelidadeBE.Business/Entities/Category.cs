namespace FidelidadeBE.Business.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public virtual IEnumerable<Category_SubCategory?> SubCategories { get; private set; }
    public virtual Category_SubCategory? DependentCategory { get; private set; }
    public virtual IEnumerable<Product?> Products { get; private set; }

    public Category()
    {
    }

    public Category(string name, int level)
    {
        Name = name;
        Level = level;
    }
}