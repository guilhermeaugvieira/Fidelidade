using AutoMapper;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Models.OrderDetail;

namespace FidelidadeBE.Application.AutoMapper;

public class OrderDetailProfile : Profile
{
    public OrderDetailProfile()
    {
        CreateMap<OrderDetail, AddOrderDetailResponseModel>()
            .ForMember(x => x.Product,
                options => options.MapFrom(x => x.Product!.Product));

        CreateMap<OrderDetail, UpdateOrderDetailResponseModel>()
            .ForMember(x => x.Product,
                options => options.MapFrom(x => x.Product!.Product));
    }
}