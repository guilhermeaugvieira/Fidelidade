using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;

namespace FidelidadeBE.Data.Interfaces;

public interface IProductRepository
{
    Task AddAsync(Product product);

    Task<Product?> GetAsync(Expression<Func<Product, bool>> filter, bool isTrackingDisabled = false);

    Task<IEnumerable<Product>> GetManyAsync(Expression<Func<Product, bool>> filter,
        bool isTrackingDisabled = false);
}