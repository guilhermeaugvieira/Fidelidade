using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly DbSet<Company> _db;
    private readonly IBaseRepository<Company> _baseRepository;

    public CompanyRepository(ApplicationContext context, IBaseRepository<Company> baseRepository)
    {
        _db = context.Set<Company>();
        _baseRepository = baseRepository;
    }

    public async Task AddAsync(Company company)
    {
        await _baseRepository.AddAsync(company);
    }

    public async Task<Company?> GetAsync(Expression<Func<Company, bool>> filter, bool isTrackingDisabled = false)
    {
        var query = _db
            .Include(x => x.User)
            .Where(filter).AsQueryable();

        if (isTrackingDisabled)
            query = query.AsNoTrackingWithIdentityResolution();

        return await query.FirstOrDefaultAsync();
    }
}