using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace FidelidadeBE.API.Configurations;

public static class ConfigureWebApi
{
    public static void AddWebApiConfig(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true; //Ignora a validação do modelo
        });

        services.AddCors(options =>
        {
            options.AddPolicy("Development", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );
        });

        services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
    }

    public static void UseWebApiConfig(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}