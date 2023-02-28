using System.Security.Claims;
using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Interfaces;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using FidelidadeBE.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FidelidadeBE.Application.Services;

public class IdentityApplicationService : IIdentityApplicationService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly INotificator _notificator;
    private readonly IIdentityRepository _identityRepository;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IUser _aspNetUser;
    private readonly IUserRepository _userRepository;
    private readonly IClientRepository _clientRepository;
    private readonly ICompanyRepository _companyRepository;

    public IdentityApplicationService(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        INotificator notificator,
        IIdentityRepository identityRepository,
        SignInManager<IdentityUser> signInManager,
        IUser aspNetUser,
        IUserRepository userRepository,
        IClientRepository clientRepository,
        ICompanyRepository companyRepository
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _notificator = notificator;
        _identityRepository = identityRepository;
        _signInManager = signInManager;
        _aspNetUser = aspNetUser;
        _userRepository = userRepository;
        _clientRepository = clientRepository;
        _companyRepository = companyRepository;
    }

    public async Task BeginTransactionAsync()
    {
        await _identityRepository.BeginTransactionAsync();
    }

    public async Task CommitChangesAsync()
    {
        await _identityRepository.CommitAsync();
    }

    public async Task RollbackChangesAsync()
    {
        await _identityRepository.RollbackAsync();
    }

    public IdentityUser GenerateIdentityUser(string email, bool emailConfirmed)
    {
        return new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = emailConfirmed
        };
    }

    public async Task<bool> CreateUserAsync(IdentityUser identityUser, string password)
    {
        var identityUserAdded = await _userManager.CreateAsync(identityUser, password);

        if (identityUserAdded.Succeeded) return true;

        foreach (var error in identityUserAdded.Errors)
            _notificator.AddNotification(error.Description, NotificationType.BusinessRules);

        return false;
    }

    public async Task<IdentityRole> CreateRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);

        if (role != null) return role;

        role = new IdentityRole(roleName);
        await _roleManager.CreateAsync(role);

        return role;
    }

    public async Task AddClaimToRoleAsync(IdentityRole role, string claimType, string claimValue)
    {
        var claim = new Claim(claimType, claimValue);

        var claimCheck = (await _roleManager.GetClaimsAsync(role)).Any(x => x.Type == claimType && x.Value == claimValue);

        if (!claimCheck) await _roleManager.AddClaimAsync(role, claim);
    }

    public async Task AddRoleToUserAsync(IdentityUser user, string roleName)
    {
        if (!await _userManager.IsInRoleAsync(user, roleName))
            await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<IdentityUser?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);

        if (identityUser == null)
        {
            _notificator.AddNotification("Login doesn't exist", NotificationType.IncorrectData);
            return false;
        }

        var loggedInUser = await _signInManager.PasswordSignInAsync(identityUser, password, false, false);

        if (loggedInUser.Succeeded) return true;

        _notificator.AddNotification("Username or Password incorrect", NotificationType.IncorrectData);
        return false;
    }

    public async Task<User?> GetLoggedInUserWithAddressOfRelationsAsync()
    {
        return await _userRepository.GetUserWithAddressOfRelationsAsync(x => x.IdentityId == _aspNetUser.GetUserId());
    }

    public async Task<User?> GetLoggedInUserAsync()
    {
        return await _userRepository.GetAsync(x => x.IdentityId == _aspNetUser.GetUserId());
    }

    public async Task<Client?> GetClientLoggedInAsync(bool isTrackingDisabled = false)
    {
        var loggedInUser = await GetLoggedInUserAsync();

        if (loggedInUser == null) return null;

        return await _clientRepository.GetAsync(x => x.User!.Id == loggedInUser.Id, isTrackingDisabled);
    }

    public async Task<Company?> GetCompanyLoggedInAsync(bool isTrackingDisabled = false)
    {
        var loggedInUser = await GetLoggedInUserAsync();

        if (loggedInUser == null) return null;

        return await _companyRepository.GetAsync(x => x.User!.Id == loggedInUser.Id, isTrackingDisabled);
    }
}