using FidelidadeBE.Infra.Interfaces;
using FidelidadeBE.Infra.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FidelidadeBE.Infra.Services;

public class JwtService : IJwtService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtModel _jwtSettings;

    public JwtService(
        UserManager<IdentityUser> userManager,
        IConfiguration configuration)
    {
        _userManager = userManager;

        var jwtSection = configuration.GetSection("JwtSettings");
        _jwtSettings = jwtSection.Get<JwtModel>();
    }

    public async Task<string> GenerateJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
            ClaimValueTypes.Integer64));

        foreach (var userRole in userRoles) claims.Add(new Claim("role", userRole));

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Emitter,
            Audience = _jwtSettings.ValidIn,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.HoursToExpire),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Subject = identityClaims
        });

        var encodedToken = tokenHandler.WriteToken(token);

        return encodedToken;
    }

    private static long ToUnixEpochDate(DateTime date)
    {
        return (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);
    }
}