using FidelidadeBE.Business.Models.Base;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FidelidadeBE.API.Controllers;

[Produces("application/json")]
[Route("v{version:ApiVersion}/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    private readonly INotificator _notificator;

    protected BaseController(INotificator notificator)
    {
        _notificator = notificator;
    }

    protected void NotifyInvalidModelError(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
            _notificator.AddNotification(errorMessage, NotificationType.IncorrectData);
        }
    }

    protected IEnumerable<string> GetErrors()
    {
        return _notificator.GetNotifications().Select(x => x.Message);
    }

    protected ActionResult BaseResponse<TResult>(TResult resultData)
    {
        if (resultData != null) return Ok(new SuccessVM<TResult>(resultData));

        if (!_notificator.HasNotification()) return NoContent();

        var notifications = _notificator.GetNotifications().ToList();

        var errors = notifications.Select(x => x.Message);
        var errorTypes = notifications.Select(x => x.Type);

        var notificationTypes = errorTypes.ToList();
        if (notificationTypes.Any(x => x == NotificationType.NotFoundResource)) return NotFound(new ErrorVM(errors));
        if (notificationTypes.Any(x => x == NotificationType.IncorrectData)) return BadRequest(new ErrorVM(errors));
        if (notificationTypes.Any(x => x == NotificationType.BusinessRules)) return Conflict(new ErrorVM(errors));

        return Ok(new SuccessVM<TResult>(resultData));
    }
}