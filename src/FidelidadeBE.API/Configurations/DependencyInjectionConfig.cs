using FidelidadeBE.Application.Extensions;
using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Application.Services;
using FidelidadeBE.Business.Interfaces;
using FidelidadeBE.Business.Services;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using FidelidadeBE.Data.Interfaces;
using FidelidadeBE.Data.Repositories;
using FidelidadeBE.Infra.Interfaces;
using FidelidadeBE.Infra.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FidelidadeBE.API.Configurations;

public static class ConfigureDependencyInjection
{
    public static void AddDependencyInjectionConfig(this IServiceCollection services)
    {
        #region Application Resources

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwagger>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<INotificator, Notificator>();
        services.AddScoped<IUser, AspNetUser>();

        #endregion

        #region Domain Services

        services.AddScoped<IDomainBaseService, DomainBaseService>();

        #endregion

        #region Repositories

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IIdentityRepository, IdentityRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategory_SubCategoryRepository, Category_SubCategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPoint_CompanyRepository, Point_CompanyRepository>();
        services.AddScoped<IPointRepository, PointRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

        #endregion

        #region Application Services

        services.AddScoped<IClientApplicationService, ClientApplicationService>();
        services.AddScoped<IAddressApplicationService, AddressApplicationService>();
        services.AddScoped<IAdministratorApplicationService, AdministratorApplicationService>();
        services.AddScoped<IIdentityApplicationService, IdentityApplicationService>();
        services.AddScoped<IAccessApplicationService, AccessApplicationService>();
        services.AddScoped<ICompanyApplicationService, CompanyApplicationService>();
        services.AddScoped<IProductApplicationService, ProductApplicationService>();
        services.AddScoped<IPointApplicationService, PointApplicationService>();
        services.AddScoped<IOrderDetailApplicationService, OrderDetailApplicationService>();

        #endregion
    }
}