using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Models.Address;
using FidelidadeBE.Business.Models.Base;
using FidelidadeBE.Business.Models.OrderDetail;
using FidelidadeBE.Business.Models.Product;
using FidelidadeBE.Business.Models.User;
using FidelidadeBE.Business.Types.OrderDetail;
using FidelidadeBE.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FidelidadeBE.API.Controllers.V1;

[Authorize(Roles = "Administrator")]
[ApiVersion("1.0")]
public class AdministratorController : BaseController
{
    private readonly IAdministratorApplicationService _administratorApplicationService;
    private readonly IProductApplicationService _productApplicationService;
    private readonly IOrderDetailApplicationService _orderDetailApplicationService;

    public AdministratorController(
        INotificator notificator,
        IAdministratorApplicationService administratorApplicationService,
        IProductApplicationService productApplicationService,
        IOrderDetailApplicationService orderDetailApplicationService
    ) : base(notificator)
    {
        _administratorApplicationService = administratorApplicationService;
        _productApplicationService = productApplicationService;
        _orderDetailApplicationService = orderDetailApplicationService;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<AddUserResponseModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorVM))]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<SuccessVM<AddUserResponseModel>>> AddAdministrator(AddUserRequestModel administratorUserInfo)
    {
        if (ModelState is not {IsValid: true})
        {
            NotifyInvalidModelError(ModelState);
            return BadRequest(new ErrorVM(GetErrors()));
        }

        var response = await _administratorApplicationService.AddAdministratorUserAsync(administratorUserInfo);

        return BaseResponse(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<AddProductRequestModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorVM))]
    [HttpPost("Product")]
    public async Task<ActionResult<SuccessVM<AddProductRequestModel>>> AddProduct(AddProductRequestModel product)
    {
        if (ModelState is not {IsValid: true})
        {
            NotifyInvalidModelError(ModelState);
            return BadRequest(new ErrorVM(GetErrors()));
        }

        var response = await _productApplicationService.AddProductAsync(product);

        return BaseResponse(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<IEnumerable<AddProductResponseModel>>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("Product/Available")]
    public async Task<ActionResult<SuccessVM<IEnumerable<AddProductResponseModel>>>> GetAvailableRedeemProducts()
    {
        var response = await _productApplicationService.GetAvailableProductsAsync();

        return BaseResponse(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<UpdateOrderDetailResponseModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorVM))]
    [HttpPut("Order/{orderId:guid}/Status")]
    public async Task<ActionResult<SuccessVM<UpdateOrderDetailResponseModel>>> UpdateOrderDetailStatus(Guid orderId,
        DeliveryStatusType status)
    {
        if (ModelState is not {IsValid: true})
        {
            NotifyInvalidModelError(ModelState);
            return BadRequest(new ErrorVM(GetErrors()));
        }

        var response = await _orderDetailApplicationService.UpdateOrderStatus(orderId, status);

        return BaseResponse(response);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<IEnumerable<UpdateOrderDetailResponseModel>>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("Order")]
    public async Task<ActionResult<SuccessVM<IEnumerable<UpdateOrderDetailResponseModel>>>> GetOrders()
    {
        var response = await _orderDetailApplicationService.GetOrders();

        return BaseResponse(response);
    }
    
    
}