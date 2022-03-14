using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbSet<Category> _db;
    private readonly IBaseRepository<Category> _baseRepository;

    public CategoryRepository(ApplicationContext context, IBaseRepository<Category> baseRepository)
    {
        _db = context.Set<Category>();
        _baseRepository = baseRepository;
    }

    public async Task AddAsync(Category category)
    {
        await _baseRepository.AddAsync(category);
    }

    public async Task<Category?> GetAsync(Expression<Func<Category, bool>> filter, bool isTrackingDisabled = false)
    {
        return await _baseRepository.GetAsync(filter, isTrackingDisabled);
    }
}