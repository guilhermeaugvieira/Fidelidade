namespace FidelidadeBE.Business.Models.Point;

public class PointReport_ProductCategoryResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}