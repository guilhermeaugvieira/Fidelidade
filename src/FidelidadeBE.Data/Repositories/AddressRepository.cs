using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Context;
using FidelidadeBE.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly DbSet<Address> _db;
    private readonly IBaseRepository<Address> _baseRepository;

    public AddressRepository(ApplicationContext context, IBaseRepository<Address> baseRepository)
    {
        _db = context.Set<Address>();
        _baseRepository = baseRepository;
    }

    public void Update(Address address)
    {
        _baseRepository.Update(address);
    }
}