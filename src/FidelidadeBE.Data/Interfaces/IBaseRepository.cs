using FidelidadeBE.Business.Entities;
using System.Linq.Expressions;

namespace FidelidadeBE.Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity);

    Task AddManyAsync(List<TEntity> entityList);

    void Update(TEntity entity);

    void UpdateMany(List<TEntity> entityList);

    void Delete(TEntity entity);

    void DeleteMany(List<TEntity> entityList);

    Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter, bool isTrackingDisabled = false);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool isTrackingDisabled = false);
}