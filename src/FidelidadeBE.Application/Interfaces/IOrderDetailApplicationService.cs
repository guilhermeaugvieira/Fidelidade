using FidelidadeBE.Business.Models.OrderDetail;
using FidelidadeBE.Business.Types.OrderDetail;

namespace FidelidadeBE.Application.Interfaces;

public interface IOrderDetailApplicationService
{
    Task<UpdateOrderDetailResponseModel?> UpdateOrderStatus(Guid orderDetailId, DeliveryStatusType deliveryStatus);

    Task<IEnumerable<UpdateOrderDetailResponseModel>?> GetAllClientOrders();

    Task<IEnumerable<UpdateOrderDetailResponseModel>?> GetOrders();
}