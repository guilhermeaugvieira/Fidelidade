using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;

namespace FidelidadeBE.Data.Interfaces;

public interface ICompanyRepository
{
    Task AddAsync(Company company);

    Task<Company?> GetAsync(Expression<Func<Company, bool>> filter, bool isTrackingDisabled = false);
}