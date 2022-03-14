using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;

namespace FidelidadeBE.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;

    public UnitOfWork(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public Task Rollback()
    {
        return Task.CompletedTask;
    }
}