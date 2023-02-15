namespace FidelidadeBE.Business.Entities;

public class Point_Company : BaseEntity
{
    public Guid PointId { get; private set; }
    public Guid CompanyId { get; private set; }
    public virtual Point? Point { get; private set; }
    public virtual Company? Company { get; private set; }

    public Point_Company(Point point, Company company)
    {
        Point = point;
        PointId = point.Id;
        Company = company;
        CompanyId = company.Id;
    }

    protected Point_Company() { }
}