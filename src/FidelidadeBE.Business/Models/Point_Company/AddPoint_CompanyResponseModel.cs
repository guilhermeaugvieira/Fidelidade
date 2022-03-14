namespace FidelidadeBE.Business.Models.Point_Company;

public class AddPoint_CompanyResponseModel
{
    public Guid Id { get; set; }
    public int AssignedPoints { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public virtual Point_CompanyAdd_ClientResponseModel Client { get; set; }
}