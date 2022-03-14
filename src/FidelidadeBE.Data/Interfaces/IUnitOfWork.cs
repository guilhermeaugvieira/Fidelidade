namespace FidelidadeBE.Data.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
    Task Rollback();
}