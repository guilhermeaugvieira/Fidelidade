using AutoMapper;
using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Entities.Validations;
using FidelidadeBE.Business.Interfaces;
using FidelidadeBE.Business.Models.OrderDetail;
using FidelidadeBE.Business.Types.OrderDetail;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using FidelidadeBE.Data.Interfaces;

namespace FidelidadeBE.Application.Services;

public class OrderDetailApplicationService : IOrderDetailApplicationService
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IDomainBaseService _domainBaseService;
    private readonly INotificator _notificator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityApplicationService _identityApplicationService;

    public OrderDetailApplicationService(
        IOrderDetailRepository orderDetailRepository,
        IDomainBaseService domainBaseService,
        INotificator notificator,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IIdentityApplicationService identityApplicationService)
    {
        _orderDetailRepository = orderDetailRepository;
        _domainBaseService = domainBaseService;
        _notificator = notificator;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityApplicationService = identityApplicationService;
    }

    public async Task<UpdateOrderDetailResponseModel?> UpdateOrderStatus(Guid orderDetailId,
        DeliveryStatusType deliveryStatus)
    {
        var orderDetail = await _orderDetailRepository.GetAsync(x => x.Id == orderDetailId);

        if (orderDetail == null)
        {
            _notificator.AddNotification("The order detail doesn't exist", NotificationType.NotFoundResource);
            return null;
        }

        if (orderDetail.DeliveryStatus == DeliveryStatusType.Enviado.ToString())
        {
            _notificator.AddNotification("The order already was sent", NotificationType.BusinessRules);
            return null;
        }

        orderDetail.UpdateDeliveryStatus(deliveryStatus);

        if (!_domainBaseService.IsEntityValid(new OrderDetailValidation(), orderDetail))
            return null;

        _orderDetailRepository.UpdateAsync(orderDetail);

        if (await _unitOfWork.CommitAsync()) return _mapper.Map<UpdateOrderDetailResponseModel>(orderDetail);

        _notificator.AddNotification("Any changes were done at the database", NotificationType.IncorrectData);
        return null;
    }

    public async Task<IEnumerable<UpdateOrderDetailResponseModel>?> GetAllClientOrders()
    {
        var client = await _identityApplicationService.GetClientLoggedInAsync();

        if (client == null)
        {
            _notificator.AddNotification("There isn't any user logged in", NotificationType.BusinessRules);
            return null;
        }

        var orders = await _orderDetailRepository.GetManyAsync(x => x.Product!.Point!.Client!.Id == client.Id);

        return !orders.Any() ? null : _mapper.Map<IEnumerable<UpdateOrderDetailResponseModel>>(orders);
    }

    public async Task<IEnumerable<UpdateOrderDetailResponseModel>?> GetOrders()
    {
        var orders = await _orderDetailRepository.GetManyAsync(x => x.Id != Guid.Empty);

        return !orders.Any() ? null : _mapper.Map<IEnumerable<UpdateOrderDetailResponseModel>>(orders);
    }
    
}