using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Models.Address;
using FidelidadeBE.Business.Models.Base;
using FidelidadeBE.Business.Models.Client;
using FidelidadeBE.Business.Models.OrderDetail;
using FidelidadeBE.Business.Models.Point;
using FidelidadeBE.Business.Models.Product;
using FidelidadeBE.Business.Types.OrderDetail;
using FidelidadeBE.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FidelidadeBE.API.Controllers.V1;

[Authorize(Roles = "Client")]
[ApiVersion("1.0")]
public class ClientController : BaseController
{
    private readonly IClientApplicationService _clientApplicationService;
    private readonly IAddressApplicationService _addressApplicationService;
    private readonly IProductApplicationService _productApplicationService;
    private readonly IPointApplicationService _pointApplicationService;
    private readonly IOrderDetailApplicationService _orderDetailApplicationService;

    public ClientController(
        INotificator notificator,
        IClientApplicationService clientApplicationService,
        IAddressApplicationService addressApplicationService,
        IProductApplicationService productApplicationService,
        IPointApplicationService pointApplicationService,
        IOrderDetailApplicationService orderDetailApplicationService) :
        base(notificator)
    {
        _clientApplicationService = clientApplicationService;
        _addressApplicationService = addressApplicationService;
        _productApplicationService = productApplicationService;
        _pointApplicationService = pointApplicationService;
        _orderDetailApplicationService = orderDetailApplicationService;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<AddClientResponseModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorVM))]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<AddClientResponseModel>> AddClient(AddClientRequestModel clientInfo)
    {
        if (!ModelState.IsValid)
        {
            NotifyInvalidModelError(ModelState);
            return BadRequest(new ErrorVM(GetErrors()));
        }

        var response = await _clientApplicationService.AddClientAsync(clientInfo);

        return BaseResponse(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<AddAddressResponseModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorVM))]
    [HttpPut("Address")]
    public async Task<ActionResult<AddAddressResponseModel>> UpdateAddress(AddAddressRequestModel newAddress)
    {
        if (!ModelState.IsValid)
        {
            NotifyInvalidModelError(ModelState);
            return BadRequest(new ErrorVM(GetErrors()));
        }

        var response = await _addressApplicationService.UpdateAddressAsync(newAddress, "Client");

        return BaseResponse(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<IEnumerable<AddProductResponseModel>>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("Product/Available")]
    public async Task<ActionResult<AddAddressResponseModel>> GetAvailableRedeemProducts()
    {
        var response = await _productApplicationService.GetAvailableProductsAsync();

        return BaseResponse(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<AddOrderDetailResponseModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorVM))]
    [HttpPost("Product/{productId:guid}/Redeem")]
    public async Task<ActionResult<AddAddressResponseModel>> RedeemProduct(Guid productId)
    {
        if (!ModelState.IsValid)
        {
            NotifyInvalidModelError(ModelState);
            return BadRequest(new ErrorVM(GetErrors()));
        }

        var response = await _pointApplicationService.RedeemProduct(productId);

        return BaseResponse(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<UpdateOrderDetailResponseModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorVM))]
    [HttpPut("Order/{orderId:guid}/ConfirmDelivery")]
    public async Task<ActionResult<AddProductResponseModel>> UpdateOrderDetailStatus(Guid orderId)
    {
        if (!ModelState.IsValid)
        {
            NotifyInvalidModelError(ModelState);
            return BadRequest(new ErrorVM(GetErrors()));
        }

        var response = await _orderDetailApplicationService.UpdateOrderStatus(orderId, DeliveryStatusType.Enviado);

        return BaseResponse(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<IEnumerable<UpdateOrderDetailResponseModel>>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorVM))]
    [HttpGet("Order")]
    public async Task<ActionResult<AddProductResponseModel>> GetAllClientOrders()
    {
        var response = await _orderDetailApplicationService.GetAllClientOrders();

        return BaseResponse(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<PointReportResponseModel>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorVM))]
    [HttpGet("Point/Report")]
    public async Task<ActionResult<AddProductResponseModel>> GetPointReport()
    {
        var response = await _pointApplicationService.GeneratePointReport();

        return BaseResponse(response);
    }
}