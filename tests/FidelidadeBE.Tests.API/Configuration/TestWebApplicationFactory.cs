using System;
using System.Linq;
using FidelidadeBE.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace FidelidadeBE.Tests.API.Configuration;

public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private readonly string _testName;
    
    public TestWebApplicationFactory(string testName)
    {
        _testName = testName;
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var applicationContextOptions = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
            
            var identityContextOptions = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<IdentityContext>));

            if (applicationContextOptions != null) services.Remove(applicationContextOptions);
            if (identityContextOptions != null) services.Remove(identityContextOptions);

            var currentDateTime = DateTime.Now;
            
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseInMemoryDatabase(
                        $"{_testName}_ApplicationDatabaseTest")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseInMemoryDatabase(
                        $"{_testName}_IdentityDatabaseTest")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            services.BuildServiceProvider();
        });
    }
}