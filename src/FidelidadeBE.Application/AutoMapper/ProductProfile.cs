using AutoMapper;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Models.OrderDetail;
using FidelidadeBE.Business.Models.Product;

namespace FidelidadeBE.Application.AutoMapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, AddProductResponseModel>();
        CreateMap<Product, OrderDetailAdd_ProductResponseModel>();
        CreateMap<Product, OrderDetailUpdate_ProductResponseModel>();
    }
}