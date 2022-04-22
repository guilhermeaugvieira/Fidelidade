using System;
using System.Linq;
using FidelidadeBE.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FidelidadeBE.Tests.API.Configuration;

public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{ 
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var applicationContextOptions = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
            
            var identityContextOptions = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<IdentityContext>));
            
            services.Remove(applicationContextOptions);
            services.Remove(identityContextOptions);

            var currentDateTime = DateTime.Now;
            
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseInMemoryDatabase($"{currentDateTime.ToString("MM/dd/yyyy hh:mm:ss.fff tt").Replace(' ', '_')}_DatabaseTest");
            });
            
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseInMemoryDatabase($"{currentDateTime.ToString("MM/dd/yyyy hh:mm:ss.fff tt").Replace(' ', '_')}_DatabaseTest");
            });

            services.BuildServiceProvider();
        });
    }
}