namespace FidelidadeBE.Business.Entities;

public class Point_Product : BaseEntity
{
    public Guid ProductId { get; private set; }
    public Guid PointId { get; private set; }
    public virtual Product? Product { get; private set; }
    public virtual Point? Point { get; private set; }
    public virtual OrderDetail? OrderDetail { get; private set; }

    public Point_Product(Product product, Point point)
    {
        Product = product;
        Point = point;

        ProductId = product.Id;
        PointId = point.Id;
    }

    protected Point_Product() {}
}