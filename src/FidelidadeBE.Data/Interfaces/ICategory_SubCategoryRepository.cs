using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;

namespace FidelidadeBE.Data.Interfaces;

public interface ICategory_SubCategoryRepository
{
    Task AddAsync(Category_SubCategory categorySubCategory);

    Task<Category_SubCategory?> GetAsync(Expression<Func<Category_SubCategory, bool>> filter,
        bool isTrackingDisabled = false);
}