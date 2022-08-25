using FidelidadeBE.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.API.Configurations;

public static class ConfigureContext
{
    public static void AddContextConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseMySql(configuration.GetConnectionString("MySql"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("MySql")))
                .EnableSensitiveDataLogging();
        });
        
        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseMySql(configuration.GetConnectionString("MySql"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("MySql")))
                .EnableSensitiveDataLogging();
        });
    }
}