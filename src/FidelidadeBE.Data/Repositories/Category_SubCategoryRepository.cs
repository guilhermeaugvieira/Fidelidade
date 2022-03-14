using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Repositories;

public class Category_SubCategoryRepository : ICategory_SubCategoryRepository
{
    private readonly DbSet<Category_SubCategory> _db;
    private readonly IBaseRepository<Category_SubCategory> _baseRepository;

    public Category_SubCategoryRepository(ApplicationContext context,
        IBaseRepository<Category_SubCategory> baseRepository)
    {
        _baseRepository = baseRepository;
        _db = context.Set<Category_SubCategory>();
    }

    public async Task AddAsync(Category_SubCategory categorySubCategory)
    {
        await _baseRepository.AddAsync(categorySubCategory);
    }

    public async Task<Category_SubCategory?> GetAsync(Expression<Func<Category_SubCategory, bool>> filter,
        bool isTrackingDisabled = false)
    {
        var query = _db
            .Include(x => x.ParentCategory)
            .Include(x => x.SubCategory)
            .Where(filter).AsQueryable();

        if (isTrackingDisabled)
            query = query.AsNoTrackingWithIdentityResolution();

        return await query
            .FirstOrDefaultAsync();
    }
}