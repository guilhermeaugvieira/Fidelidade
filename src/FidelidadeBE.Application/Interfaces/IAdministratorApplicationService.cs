using FidelidadeBE.Business.Models.User;

namespace FidelidadeBE.Application.Interfaces;

public interface IAdministratorApplicationService
{
    Task<AddUserResponseModel?> AddAdministratorUserAsync(AddUserRequestModel administratorUser);
}