using System.Collections.Generic;
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

namespace FidelidadeBE.Tests.API;

public class AccessControllerTests
{
    [Fact]
    public async Task LoginSuccessfull()
    {
        var webApplicationFactory = new TestWebApplicationFactory<Program>();
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
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
        Assert.NotEqual(0, response.Data.Length);
    }
    
    [Fact]
    public async Task LoginError_WhenUserDoesntExist()
    {
        var webApplicationFactory = new TestWebApplicationFactory<Program>();
        var client = webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
        });
        
        //Arrange
        var userAccess = new UserAccessRequestModel()
        {
            Email = "administratorx@application.com",
            Password = "P@ssw0rdTeste",
        };

        var expectedReturn = new ErrorVM(new List<string>() {"Login doesn't exist"});

        //Act
        var requestResult = await client.PostAsJsonAsync(
            "/v1.0/Access/Login",
            userAccess
        );

        var response = JsonConvert.DeserializeObject<ErrorVM>(await requestResult.Content.ReadAsStringAsync());
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, requestResult.StatusCode);
        Assert.Equal(JsonConvert.SerializeObject(expectedReturn), JsonConvert.SerializeObject(response));
    }
    
    [Fact]
    public async Task LoginError_WhenPasswordIsIncorrect()
    {
        var webApplicationFactory = new TestWebApplicationFactory<Program>();
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
            Password = "P@ssw0rdTeste2"
        };
        
        var expectedReturn = new ErrorVM(new List<string>() {"Username or Password incorrect"});

        //Act
        var loginResult = await client.PostAsJsonAsync(
            "/v1.0/Access/Login",
            newAdministratorLogin
        );

        var response = JsonConvert.DeserializeObject<ErrorVM>(await loginResult.Content.ReadAsStringAsync());
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, loginResult.StatusCode);
        Assert.Equal(JsonConvert.SerializeObject(expectedReturn), JsonConvert.SerializeObject(response));
    }
}