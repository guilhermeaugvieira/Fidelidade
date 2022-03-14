namespace FidelidadeBE.Business.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; }
    public int Points { get; private set; }
    public Guid CategoryId { get; private set; }
    public virtual Category? Category { get; private set; }
    public virtual Point_Product? Point { get; private set; }

    public Product()
    {
    }

    public Product(string name, int points, Category category)
    {
        Name = name;
        Points = points;
        Category = category;

        CategoryId = category.Id;
    }
}