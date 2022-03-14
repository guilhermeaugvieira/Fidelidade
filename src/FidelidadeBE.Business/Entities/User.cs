namespace FidelidadeBE.Business.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; }
    public Guid IdentityId { get; private set; }
    public virtual Client? Client { get; private set; }
    public virtual Company? Company { get; private set; }

    public User()
    {
    }

    public User(string name)
    {
        Name = name;
    }

    public void SetIdentityId(Guid identityId)
    {
        IdentityId = identityId;
    }
}