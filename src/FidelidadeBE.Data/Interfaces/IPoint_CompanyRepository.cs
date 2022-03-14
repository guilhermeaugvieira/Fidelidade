using FidelidadeBE.Business.Entities;

namespace FidelidadeBE.Data.Interfaces;

public interface IPoint_CompanyRepository
{
    Task AddAsync(Point_Company pointCompany);
}