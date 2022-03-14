using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Repositories;

public class Point_CompanyRepository : IPoint_CompanyRepository
{
    private readonly DbSet<Point_Company> _db;
    private readonly IBaseRepository<Point_Company> _baseRepository;

    public Point_CompanyRepository(ApplicationContext context, IBaseRepository<Point_Company> baseRepository)
    {
        _db = context.Set<Point_Company>();
        _baseRepository = baseRepository;
    }

    public async Task AddAsync(Point_Company pointCompany)
    {
        await _baseRepository.AddAsync(pointCompany);
    }
}