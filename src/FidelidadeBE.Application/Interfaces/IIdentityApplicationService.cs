using FidelidadeBE.Business.Entities;
using Microsoft.AspNetCore.Identity;

namespace FidelidadeBE.Application.Interfaces;

public interface IIdentityApplicationService
{
    Task BeginTransactionAsync();

    Task CommitChangesAsync();

    Task RollbackChangesAsync();

    IdentityUser GenerateIdentityUser(string email, bool emailConfirmed);

    Task<bool> CreateUserAsync(IdentityUser identityUser, string password);

    Task<IdentityRole> CreateRoleAsync(string roleName);

    Task AddClaimToRoleAsync(IdentityRole role, string claimType, string claimValue);

    Task AddRoleToUserAsync(IdentityUser user, string roleName);

    Task<IdentityUser?> GetUserByEmailAsync(string email);

    Task<bool> LoginAsync(string email, string password);

    Task<User?> GetLoggedInUserWithAddressOfRelationsAsync();

    Task<User?> GetLoggedInUserAsync();

    Task<Client?> GetClientLoggedInAsync(bool isTrackingDisabled = false);

    Task<Company?> GetCompanyLoggedInAsync(bool isTrackingDisabled = false);
}