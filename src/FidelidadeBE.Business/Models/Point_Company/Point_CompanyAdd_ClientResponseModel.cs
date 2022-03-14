namespace FidelidadeBE.Business.Models.Point_Company;

public class Point_CompanyAdd_ClientResponseModel
{
    public Guid Id { get; set; }
    public string CPF { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Point_CompanyAdd_UserResponseModel User { get; set; }
}