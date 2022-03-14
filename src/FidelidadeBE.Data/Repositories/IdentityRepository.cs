using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;

namespace FidelidadeBE.Data.Repositories;

public class IdentityRepository : IIdentityRepository
{
    private readonly IdentityContext _identityContext;

    public IdentityRepository(IdentityContext identityContext)
    {
        _identityContext = identityContext;
    }

    public async Task BeginTransactionAsync()
    {
        await _identityContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _identityContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackAsync()
    {
        await _identityContext.Database.RollbackTransactionAsync();
    }
}