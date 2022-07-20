using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FidelidadeBE.Business.Models.Access;
using FidelidadeBE.Business.Models.Base;
using FidelidadeBE.Business.Models.User;
using FidelidadeBE.Tests.API.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace FidelidadeBE.Tests.API;

public class AccessControllerTests
{
    private readonly ITestOutputHelper _output;

    public AccessControllerTests(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public async Task LoginSuccessfully()
    {
        var webApplicationFactory = new TestWebApplicationFactory<Program>("LoginSuccessfully");
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
        
        await client.PostAsJsonAsync(
            "/v1.0/Administrator",
            newAdministrator
        );

        var newAdministratorLogin = new UserAccessRequestModel()
        {
            Email = "administrator@application.com",
            Password = "P@ssw0rdTeste"
        };

        //Act
        var loginResult = await client.PostAsJsonAsync(
            "/v1.0/Access/Login",
            newAdministratorLogin
        );

        var response = JsonConvert.DeserializeObject<SuccessVM<string>>(await loginResult.Content.ReadAsStringAsync());
        
        _output.WriteLine($"Access Token: {response.Data}");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
        Assert.NotEqual(0, response!.Data.Length);
    }
}