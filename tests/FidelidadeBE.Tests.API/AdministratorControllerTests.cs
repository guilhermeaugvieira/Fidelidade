using System.Net;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using FidelidadeBE.Business.Models.Base;
using FidelidadeBE.Business.Models.User;
using FidelidadeBE.Tests.API.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace FidelidadeBE.Tests.API;

public class AdministratorControllerTests
{
    private readonly ITestOutputHelper _output;

    public AdministratorControllerTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task AddAdministrator()
    { 
        // Arrange
        var webApplicationFactory = new TestWebApplicationFactory<Program>("AddAdministrator");
        var client = webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
        });
        
        //Arrange
        var newAdministrator = new AddUserRequestModel()
        {
            Email = "administrator@application.com",
            Name = "Administrator",
            Password = "P@ssw0rdTeste"
        };
        
        // Act
        var addAdministratorResult = await client.PostAsJsonAsync(
            "/v1.0/Administrator",
            newAdministrator
        );

        _output.WriteLine($"Result Insert: {await addAdministratorResult.Content.ReadAsStringAsync()}");

        var addAdministratorResponse =
            JsonConvert.DeserializeObject<SuccessVM<AddUserResponseModel>>(await addAdministratorResult.Content.ReadAsStringAsync());
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, addAdministratorResult.StatusCode);
        Assert.NotNull(addAdministratorResponse!.Data);
    }
}