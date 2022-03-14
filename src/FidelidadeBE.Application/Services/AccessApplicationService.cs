using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Models.Access;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Infra.Interfaces;

namespace FidelidadeBE.Application.Services;

public class AccessApplicationService : IAccessApplicationService
{
    private readonly INotificator _notificator;
    private readonly IIdentityApplicationService _identityApplicationService;
    private readonly IJwtService _jwtService;

    public AccessApplicationService(
        INotificator notificator,
        IIdentityApplicationService identityApplicationService,
        IJwtService jwtService
    )
    {
        _notificator = notificator;
        _identityApplicationService = identityApplicationService;
        _jwtService = jwtService;
    }

    public async Task<string?> LoginAsync(UserAccessRequestModel user)
    {
        if (!await _identityApplicationService.LoginAsync(user.Email, user.Password)) return null;

        return await _jwtService.GenerateJwt(user.Email);
    }
}