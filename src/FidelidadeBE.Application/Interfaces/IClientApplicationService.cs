using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Models.Client;

namespace FidelidadeBE.Application.Interfaces;

public interface IClientApplicationService
{
    Task<AddClientResponseModel?> AddClientAsync(AddClientRequestModel clientInfo);
}