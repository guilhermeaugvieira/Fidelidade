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
                (x,res) => new Company(
                    res.Mapper.Map<Address>(x.Address),
                    new User(x.User.Name),
                    x.CNPJ
                )
            );
    }
}