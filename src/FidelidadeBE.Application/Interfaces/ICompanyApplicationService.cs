using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Models.Company;

namespace FidelidadeBE.Application.Interfaces;

public interface ICompanyApplicationService
{
    Task<AddCompanyResponseModel?> AddCompanyAsync(AddCompanyRequestModel companyInfo);
}