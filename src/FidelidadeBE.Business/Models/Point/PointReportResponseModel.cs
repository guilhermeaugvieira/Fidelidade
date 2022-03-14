namespace FidelidadeBE.Business.Models.Point;

public class PointReportResponseModel
{
    public int AvailablePoints { get; set; }
    public List<PointReport_PointResponseModel> Points { get; set; }
}