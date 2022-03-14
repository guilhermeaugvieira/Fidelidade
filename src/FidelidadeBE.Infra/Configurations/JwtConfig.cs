using FidelidadeBE.Infra.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FidelidadeBE.Infra.Configurations;

public static class JwtConfig
{
    public static void AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfigurations = configuration.GetSection("JwtSettings");
        services.Configure<JwtModel>(jwtConfigurations);
    }
}