namespace FidelidadeBE.Business.Entities;

public class Point : BaseEntity
{
    public Guid ClientId { get; private set; }
    public int AssignedPoints { get; private set; }
    public virtual Client? Client { get; private set; }
    public virtual Point_Company? Company { get; private set; }
    public virtual Point_Product? Product { get; private set; }

    public Point(Client client, int assignedPoints)
    {
        Client = client;
        ClientId = client.Id;
        AssignedPoints = assignedPoints;
    }

    protected Point() { }

}