using AutoMapper;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Models.Address;

namespace FidelidadeBE.Application.AutoMapper;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddAddressResponseModel>();

        CreateMap<AddAddressRequestModel, Address>()
            .ConstructUsing(x => new Address(x.State, x.City, x.District, x.CEP, x.Street, x.Number));
    }
}