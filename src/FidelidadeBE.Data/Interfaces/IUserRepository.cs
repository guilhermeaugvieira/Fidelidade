using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;

namespace FidelidadeBE.Data.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task<User?> GetUserWithAddressOfRelationsAsync(Expression<Func<User, bool>> filter,
        bool isTrackingDisabled = false);

    Task<User?> GetAsync(Expression<Func<User, bool>> filter,
        bool isTrackingDisabled = false);
}