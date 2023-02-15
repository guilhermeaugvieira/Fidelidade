namespace FidelidadeBE.Business.Entities;

public class Address : BaseEntity
{
    public string State { get; private set; }
    public string City { get; private set; }
    public string District { get; private set; }
    public string CEP { get; private set; }
    public string Street { get; private set; }
    public int? Number { get; private set; }
    public virtual Client? Client { get; private set; }
    public virtual Company? Company { get; private set; }

    protected Address() { }

    public Address(string state, string city, string district, string cep, string street, int? number)
    {
        State = state;
        City = city;
        District = district;
        CEP = cep;
        Street = street;
        Number = number == 0 ? null : number;
    }

    public void UpdateAddress(string state, string city, string district, string cep, string street, int? number)
    {
        State = state;
        City = city;
        District = district;
        CEP = cep;
        Street = street;
        Number = number == 0 ? null : number;

        UpdateChangeDate();
    }
}