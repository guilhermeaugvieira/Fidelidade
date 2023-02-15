using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Repositories;

public class PointRepository : IPointRepository
{
    private readonly DbSet<Point> _db;
    private readonly IBaseRepository<Point> _baseRepository;

    public PointRepository(ApplicationContext context, IBaseRepository<Point> baseRepository)
    {
        _db = context.Set<Point>();
        _baseRepository = baseRepository;
    }

    public async Task<IEnumerable<Point>> GetManyAsync(Expression<Func<Point, bool>> filter,
        bool isTrackingDisabled = false)
    {
        return await _baseRepository.GetManyAsync(filter, isTrackingDisabled);
    }

    public async Task<IEnumerable<Point>> GetPointsReport(Expression<Func<Point, bool>> filter,
        bool isTrackingDisabled = false)
    {
        var query = _db
            .Include(x => x.Company)
                .ThenInclude(x => x!.Company)
                    .ThenInclude(x => x!.Address)
            .Include(x => x.Company)
                .ThenInclude(x => x!.Company)
                    .ThenInclude(x => x!.User)
            .Include(x => x.Product)
                .ThenInclude(x => x!.Product)
                    .ThenInclude(x => x!.Category)
            .Where(filter)
            .OrderByDescending(x => x.CreatedAt).AsQueryable();

        if (isTrackingDisabled)
            query = query.AsNoTrackingWithIdentityResolution();

        return await query.ToListAsync();
    }
}