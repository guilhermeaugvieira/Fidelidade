using AutoMapper;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Models.Client;
using FidelidadeBE.Business.Models.Point_Company;

namespace FidelidadeBE.Application.AutoMapper;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Client, AddClientResponseModel>();

        CreateMap<Client, Point_CompanyAdd_ClientResponseModel>();

        CreateMap<AddClientRequestModel, Client>()
            .ConstructUsing(
                (x, res) => new Client(
                    res.Mapper.Map<Address>(x.Address),
                    new User(x.User.Name),
                    x.CPF
                )
            );
    }
}