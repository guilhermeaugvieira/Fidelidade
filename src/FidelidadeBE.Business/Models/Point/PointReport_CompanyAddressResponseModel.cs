namespace FidelidadeBE.Business.Models.Point;

public class PointReport_CompanyAddressResponseModel
{
    public Guid Id { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string CEP { get; set; }
    public string Street { get; set; }
    public int? Number { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}