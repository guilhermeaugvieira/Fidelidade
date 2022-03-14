using AutoMapper;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Models.Client;
using FidelidadeBE.Business.Models.Point_Company;
using FidelidadeBE.Business.Models.User;

namespace FidelidadeBE.Application.AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, ClientAdd_UserResponseModel>();
        CreateMap<User, AddUserResponseModel>();
        CreateMap<User, Point_CompanyAdd_UserResponseModel>();

        CreateMap<AddUserRequestModel, User>()
            .ConstructUsing(x => new User(x.Name));
    }
}