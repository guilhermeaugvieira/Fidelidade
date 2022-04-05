namespace FidelidadeBE.Business.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public virtual IEnumerable<Category_SubCategory?> SubCategories { get; }
    public virtual Category_SubCategory? DependentCategory { get; }
    public virtual IEnumerable<Product> Products { get; }

    public Category()
    {
    }

    public Category(string name, int level)
    {
        Name = name;
        Level = level;
    }
}