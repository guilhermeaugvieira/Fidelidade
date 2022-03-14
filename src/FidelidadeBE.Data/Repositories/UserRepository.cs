using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _db;
    private readonly IBaseRepository<User> _baseRepository;

    public UserRepository(ApplicationContext context, IBaseRepository<User> baseRepository)
    {
        _db = context.Set<User>();
        _baseRepository = baseRepository;
    }

    public async Task AddAsync(User user)
    {
        await _baseRepository.AddAsync(user);
    }

    public async Task<User?> GetUserWithAddressOfRelationsAsync(Expression<Func<User, bool>> filter,
        bool isTrackingDisabled = false)
    {
        var query = _db
            .Include(x => x.Client)
            .ThenInclude(x => x!.Address)
            .Include(x => x.Company)
            .ThenInclude(x => x!.Address)
            .Where(filter)
            .AsQueryable();

        if (isTrackingDisabled)
            query = query.AsNoTrackingWithIdentityResolution();

        return await query
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetAsync(Expression<Func<User, bool>> filter,
        bool isTrackingDisabled = false)
    {
        return await _baseRepository.GetAsync(filter, isTrackingDisabled);
    }
}