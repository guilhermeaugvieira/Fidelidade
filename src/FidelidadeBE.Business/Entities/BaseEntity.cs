using FidelidadeBE.Business.Helpers;

namespace FidelidadeBE.Business.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    protected BaseEntity()
    {
        Id = CombGuid.Generate();
        CreatedAt = DateTime.Now;
    }

    protected void UpdateChangeDate()
    {
        UpdatedAt = DateTime.Now;
    }
}