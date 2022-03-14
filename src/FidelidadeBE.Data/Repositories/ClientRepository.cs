using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly DbSet<Client> _db;
    private readonly IBaseRepository<Client> _baseRepository;

    public ClientRepository(ApplicationContext context, IBaseRepository<Client> baseRepository)
    {
        _db = context.Set<Client>();
        _baseRepository = baseRepository;
    }

    public async Task AddAsync(Client client)
    {
        await _baseRepository.AddAsync(client);
    }

    public async Task<Client?> GetAsync(Expression<Func<Client, bool>> filter, bool isTrackingDisabled = false)
    {
        var query = _db
            .Include(x => x.User)
            .Where(filter).AsQueryable();

        if (isTrackingDisabled)
            query = query.AsNoTrackingWithIdentityResolution();

        return await query.FirstOrDefaultAsync();
    }
}