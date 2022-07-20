using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Models.Access;
using FidelidadeBE.Business.Models.Base;
using FidelidadeBE.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FidelidadeBE.API.Controllers.V1;

[Authorize]
[ApiVersion("1.0")]
public class AccessController : BaseController
{
    private readonly IAccessApplicationService _accessApplicationService;

    public AccessController(INotificator notificator, IAccessApplicationService accessApplicationService) :
        base(notificator)
    {
        _accessApplicationService = accessApplicationService;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessVM<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorVM))]
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<SuccessVM<string>>> Login(UserAccessRequestModel user)
    {
        if (ModelState is not {IsValid: true})
        {
            NotifyInvalidModelError(ModelState);
            return BadRequest(new ErrorVM(GetErrors()));
        }

        var response = await _accessApplicationService.LoginAsync(user);

        return BaseResponse(response);
    }
}