using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using FidelidadeBE.Application.Extensions;
using Microsoft.AspNetCore.Identity;

namespace FidelidadeBE.API.Extensions;

public static class CustomAuthorization
{
    public static async Task<bool> ValidateUserClaims(HttpContext context,
        Claim claim,
        RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager)
    {
        var userHasClaim = context.User.Identity! is {IsAuthenticated: true} &&
            context.User.Claims.Any(c => c.Type == claim.Type && c.Value == claim.Value);

        if (!userHasClaim) return await VerifyClaimInUserRoles(context, claim, roleManager, userManager);

        return true;
    }

    private static async Task<bool> VerifyClaimInUserRoles(HttpContext context,
        Claim claim,
        RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager)
    {
        var loggedInUser = await userManager.FindByIdAsync(context.User.GetUserId());

        var userRoles = await userManager.GetRolesAsync(loggedInUser);

        foreach (var role in userRoles)
        {
            var verifiedRole = await roleManager.FindByNameAsync(role);

            var roleClaims = await roleManager.GetClaimsAsync(verifiedRole);

            if (roleClaims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
                return true;
        }

        return false;
    }
}

public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimFilterRequest))
    {
        Arguments = new object[]
        {
            new Claim(claimName, claimValue)
        };
    }
}

public class ClaimFilterRequest : IAsyncAuthorizationFilter
{
    private readonly Claim _claim;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public ClaimFilterRequest(Claim claim, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _claim = claim;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!(context.HttpContext.User.Identity! is {IsAuthenticated: true})) context.Result = new StatusCodeResult(401);

        if (!await CustomAuthorization.ValidateUserClaims(context.HttpContext, _claim, _roleManager, _userManager))
            context.Result = new StatusCodeResult(403);
    }
}