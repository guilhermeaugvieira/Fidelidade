using AutoMapper;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Models.Company;

namespace FidelidadeBE.Application.AutoMapper;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, AddCompanyResponseModel>();

        CreateMap<AddCompanyRequestModel, Company>()
            .ConstructUsing(
                x => new Company(
                    new Address(
                        x.Address.State,
                        x.Address.City,
                        x.Address.District,
                        x.Address.CEP,
                        x.Address.Street,
                        x.Address.Number
                    ),
                    new User(x.User.Name),
                    x.CNPJ
                )
            );
    }
}