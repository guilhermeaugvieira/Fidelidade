using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FidelidadeBE.API.Configurations;

public class ConfigureSwagger : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwagger(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "Fidelidade - API",
            Version = description.ApiVersion.ToString(),
            Description = "Fidelity Endpoints' Documentation",
            Contact = new OpenApiContact {Email = "guilhermeaugvieira@gmail.com", Name = "Guilherme Augusto Vieira"},
            TermsOfService = new Uri("https://opensource.org/licenses/MIT"),
            License = new OpenApiLicense {Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT")}
        };

        if (description.IsDeprecated) info.Description += " - Obsolete Version";

        return info;
    }
}

public static class SwaggerConfig
{
    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwaggerDefaultValues>();

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Insert JWT Token this way: Bearer {your token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
    }

    public static void UseSwaggerConfig(this IApplicationBuilder app,
        IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(
            options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
            }
        );
    }
}

public class SwaggerDefaultValues : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiDescription = context.ApiDescription;

        operation.Deprecated = apiDescription.IsDeprecated();

        if (operation.Parameters == null) return;

        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

            parameter.Description ??= description.ModelMetadata.Description;

            parameter.Required |= description.IsRequired;
        }
    }
}