using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Models.Address;
using FidelidadeBE.Business.Models.Base;
using FidelidadeBE.Business.Models.Company;
using FidelidadeBE.Business.Models.Point_Company;
using FidelidadeBE.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FidelidadeBE.API.Controllers.V1;

[Authorize(Roles = "Company")]
[ApiVersion("1.0")]
public class CompanyController : BaseController
{
    private readonly ICompanyApplicationService _companyApplicationService;
    private readonly IAddressApplicationService _addressApplicationService;
    private readonly IPointApplicationService _pointApplicationService;

    public CompanyController(INotificator notificator,
        ICompanyApplicationService companyApplicationService,
        IAddressApplicationService addressApplicationService,
        IPointApplicationService pointApplicationService) :
        base(notificator)
    {
        _companyApplicationService = companyApplicationService;
        _addressApplicationService = addressApplicationService;
        _pointApplicationService = pointApplicationService;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<AddCompanyResponseModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorVM))]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<SuccessVM<AddCompanyResponseModel>>> AddCompany(AddCompanyRequestModel companyInfo)
    {
        if (ModelState is not {IsValid: true})
        {
            NotifyInvalidModelError(ModelState);
            return BadRequest(new ErrorVM(GetErrors()));
        }

        var response = await _companyApplicationService.AddCompanyAsync(companyInfo);

        return BaseResponse(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<AddAddressResponseModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorVM))]
    [HttpPut("Address")]
    public async Task<ActionResult<SuccessVM<AddAddressResponseModel>>> UpdateAddress(AddAddressRequestModel newAddress)
    {
        if (ModelState is not {IsValid: true})
        {
            NotifyInvalidModelError(ModelState);
            return BadRequest(new ErrorVM(GetErrors()));
        }

        var response = await _addressApplicationService.UpdateAddressAsync(newAddress, "Company");

        return BaseResponse(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<AddPoint_CompanyResponseModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorVM))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorVM))]
    [HttpPost("Client/{clientCpf}/Point")]
    public async Task<ActionResult<SuccessVM<AddPoint_CompanyResponseModel>>> AssignPointsToClient(string clientCpf,
        AddPoint_CompanyRequestModel pointCompanyInfo)
    {
        if (ModelState is not {IsValid: true})
        {
            NotifyInvalidModelError(ModelState);
            return BadRequest(new ErrorVM(GetErrors()));
        }

        var response = await _pointApplicationService.AssignPointsToClient(clientCpf, pointCompanyInfo);

        return BaseResponse(response);
    }
}