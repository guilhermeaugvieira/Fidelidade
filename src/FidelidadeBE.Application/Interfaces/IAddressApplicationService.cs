using FidelidadeBE.Business.Models.Address;

namespace FidelidadeBE.Application.Interfaces;

public interface IAddressApplicationService
{
    Task<AddAddressResponseModel?> UpdateAddressAsync(AddAddressRequestModel newAddress, string role);
}