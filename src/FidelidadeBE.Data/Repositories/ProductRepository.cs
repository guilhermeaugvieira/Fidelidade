using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DbSet<Product> _db;
    private readonly IBaseRepository<Product> _baseRepository;

    public ProductRepository(ApplicationContext context, IBaseRepository<Product> baseRepository)
    {
        _db = context.Set<Product>();
        _baseRepository = baseRepository;
    }

    public async Task AddAsync(Product product)
    {
        await _baseRepository.AddAsync(product);
    }

    public async Task<Product?> GetAsync(Expression<Func<Product, bool>> filter, bool isTrackingDisabled = false)
    {
        var query = _db
            .Include(x => x.Category)
            .Include(x => x.Point)
            .Where(filter).AsQueryable();

        if (isTrackingDisabled)
            query = query.AsNoTrackingWithIdentityResolution();

        return await query
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetManyAsync(Expression<Func<Product, bool>> filter,
        bool isTrackingDisabled = false)
    {
        var query = _db
            .Include(x => x.Category)
            .Include(x => x.Point)
            .Where(filter).AsQueryable();

        if (isTrackingDisabled)
            query = query.AsNoTrackingWithIdentityResolution();

        return await query
            .ToListAsync();
    }
}