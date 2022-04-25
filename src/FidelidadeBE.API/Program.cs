using FidelidadeBE.API.Configurations;
using FidelidadeBE.API.Extensions;
using FidelidadeBE.Application.Extensions;
using FidelidadeBE.Infra.Configurations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using RT.Comb.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddContextConfig(builder.Configuration);

builder.Services.AddSwaggerConfig();

builder.Services.AddControllers();

builder.Services.AddWebApiConfig();

builder.Services.AddJwtConfig(builder.Configuration);

builder.Services.AddAutoMapperMappings();

builder.Services.AddDependencyInjectionConfig();

builder.Services.AddSqlCombGuidWithUnixDateTime();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwaggerConfig(
        app.Services.GetRequiredService<IApiVersionDescriptionProvider>()
    );
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseWebApiConfig();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();

public partial class Program {}