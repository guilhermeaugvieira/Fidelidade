namespace FidelidadeBE.Business.Entities;

public class Client : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid AddressId { get; private set; }
    public string CPF { get; private set; }
    public virtual Address? Address { get; private set; }
    public virtual User? User { get; private set; }
    public virtual IEnumerable<Point> Points { get; }

    public Client()
    {
    }

    public Client(Address address, User user, string cpf)
    {
        Address = address;
        User = user;
        AddressId = address.Id;
        UserId = user.Id;
        CPF = cpf;
    }
}