using AutoMapper;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Models.Category;

namespace FidelidadeBE.Application.AutoMapper;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, AddCategoryResponseModel>();
    }
}