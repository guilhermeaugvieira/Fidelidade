using FidelidadeBE.Business.Models.Access;

namespace FidelidadeBE.Application.Interfaces;

public interface IAccessApplicationService
{
    Task<string?> LoginAsync(UserAccessRequestModel user);
}