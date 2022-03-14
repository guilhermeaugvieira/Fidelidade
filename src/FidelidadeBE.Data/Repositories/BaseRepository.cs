using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FidelidadeBE.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _db;

    public BaseRepository(ApplicationContext context)
    {
        _db = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _db.AddAsync(entity);
    }

    public async Task AddManyAsync(List<TEntity> entityList)
    {
        await _db.AddRangeAsync(entityList);
    }

    public void Update(TEntity entity)
    {
        _db.Update(entity);
    }

    public void UpdateMany(List<TEntity> entityList)
    {
        _db.UpdateRange(entityList);
    }

    public void Delete(TEntity entity)
    {
        _db.Remove(entity);
    }

    public void DeleteMany(List<TEntity> entityList)
    {
        _db.RemoveRange(entityList);
    }

    public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter,
        bool isTrackingDisabled = false)
    {
        var query = _db.Where(filter).AsQueryable();

        if (isTrackingDisabled)
            query = query.AsNoTrackingWithIdentityResolution();

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool isTrackingDisabled = false)
    {
        var query = _db.Where(filter).AsQueryable();

        if (isTrackingDisabled)
            query = query.AsNoTrackingWithIdentityResolution();

        return await query.FirstOrDefaultAsync();
    }
}