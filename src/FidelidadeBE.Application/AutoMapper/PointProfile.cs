using AutoMapper;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Models.Point_Company;

namespace FidelidadeBE.Application.AutoMapper;

public class PointProfile : Profile
{
    public PointProfile()
    {
        CreateMap<Point, AddPoint_CompanyResponseModel>();
    }
}