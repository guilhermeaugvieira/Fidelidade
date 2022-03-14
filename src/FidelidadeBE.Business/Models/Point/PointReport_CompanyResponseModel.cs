namespace FidelidadeBE.Business.Models.Point;

public class PointReport_CompanyResponseModel
{
    public Guid Id { get; set; }
    public string CNPJ { get; set; }
    public PointReport_CompanyAddressResponseModel Address { get; set; }
    public PointReport_CompanyUserReponseModel User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}