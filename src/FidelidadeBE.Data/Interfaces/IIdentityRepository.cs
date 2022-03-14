namespace FidelidadeBE.Data.Interfaces;

public interface IIdentityRepository
{
    Task BeginTransactionAsync();

    Task CommitAsync();

    Task RollbackAsync();
}