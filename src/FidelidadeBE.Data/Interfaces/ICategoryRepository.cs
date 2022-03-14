using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;

namespace FidelidadeBE.Data.Interfaces;

public interface ICategoryRepository
{
    Task AddAsync(Category category);

    Task<Category?> GetAsync(Expression<Func<Category, bool>> filter, bool isTrackingDisabled = false);
}