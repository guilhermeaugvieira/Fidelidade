using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;

namespace FidelidadeBE.Data.Interfaces;

public interface IClientRepository
{
    Task AddAsync(Client client);

    Task<Client?> GetAsync(Expression<Func<Client, bool>> filter, bool isTrackingDisabled = false);
}