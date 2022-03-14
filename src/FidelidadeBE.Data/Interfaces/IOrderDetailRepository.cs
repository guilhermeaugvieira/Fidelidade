using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;

namespace FidelidadeBE.Data.Interfaces;

public interface IOrderDetailRepository
{
    Task AddAsync(OrderDetail orderDetail);

    void UpdateAsync(OrderDetail orderDetail);

    Task<OrderDetail?> GetAsync(Expression<Func<OrderDetail, bool>> filter, bool isTrackingDisabled = false);

    Task<IEnumerable<OrderDetail>> GetManyAsync(Expression<Func<OrderDetail, bool>> filter,
        bool isTrackingDisabled = false);
}