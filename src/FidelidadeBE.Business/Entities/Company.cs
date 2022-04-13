namespace FidelidadeBE.Business.Entities;

public class Company : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid AddressId { get; private set; }
    public string CNPJ { get; private set; }
    public virtual Address? Address { get; private set; }
    public virtual User? User { get; private set; }
    public virtual IEnumerable<Point_Company> Points { get; private set; }

    public Company()
    {
    }


    public Company(Address address, User user, string cnpj)
    {
        CNPJ = cnpj;
        Address = address;
        User = user;
        AddressId = address.Id;
        UserId = user.Id;
    }
}