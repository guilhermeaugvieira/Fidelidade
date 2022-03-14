namespace FidelidadeBE.Business.Models.Point;

public class PointReport_PointResponseModel
{
    public Guid Id { get; set; }
    public int AssignedPoints { get; set; }
    public PointReport_ProductResponseModel? Product { get; set; }
    public PointReport_CompanyResponseModel? Company { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}