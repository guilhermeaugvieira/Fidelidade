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
                x => new Client(
                    new Address(
                        x.Address.State,
                        x.Address.City,
                        x.Address.District,
                        x.Address.CEP,
                        x.Address.Street,
                        x.Address.Number
                    ),
                    new User(x.User.Name),
                    x.CPF
                )
            );
    }
}