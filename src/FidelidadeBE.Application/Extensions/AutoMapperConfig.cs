using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FidelidadeBE.Application.Extensions;

public static class ConfigureAutoMapper
{
    public static IServiceCollection AddAutoMapperMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}