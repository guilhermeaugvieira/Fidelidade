using FidelidadeBE.Business.Models.Product;

namespace FidelidadeBE.Application.Interfaces;

public interface IProductApplicationService
{
    Task<AddProductResponseModel?> AddProductAsync(AddProductRequestModel product);

    Task<IEnumerable<AddProductResponseModel>?> GetAvailableProductsAsync();
}