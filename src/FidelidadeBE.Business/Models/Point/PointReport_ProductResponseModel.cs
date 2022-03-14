namespace FidelidadeBE.Business.Models.Point;

public class PointReport_ProductResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public PointReport_ProductCategoryResponseModel Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}