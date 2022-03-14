using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly DbSet<OrderDetail> _db;
    private readonly IBaseRepository<OrderDetail> _baseRepository;

    public OrderDetailRepository(ApplicationContext context, IBaseRepository<OrderDetail> baseRepository)
    {
        _db = context.Set<OrderDetail>();
        _baseRepository = baseRepository;
    }

    public async Task AddAsync(OrderDetail orderDetail)
    {
        await _baseRepository.AddAsync(orderDetail);
    }

    public void UpdateAsync(OrderDetail orderDetail)
    {
        _baseRepository.Update(orderDetail);
    }

    public async Task<OrderDetail?> GetAsync(Expression<Func<OrderDetail, bool>> filter,
        bool isTrackingDisabled = false)
    {
        var query = _db
            .Include(x => x.Product)
            .ThenInclude(x => x!.Product)
            .Where(filter).AsQueryable();

        if (isTrackingDisabled)
            query = query.AsNoTrackingWithIdentityResolution();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<OrderDetail>> GetManyAsync(Expression<Func<OrderDetail, bool>> filter,
        bool isTrackingDisabled = false)
    {
        var query = _db
            .Include(x => x.Product)
            .ThenInclude(x => x!.Product)
            .Where(filter).AsQueryable();

        if (isTrackingDisabled)
            query = query.AsNoTrackingWithIdentityResolution();

        return await query.ToListAsync();
    }
}