using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FidelidadeBE.Business.Models.Access;
using FidelidadeBE.Business.Models.Base;
using FidelidadeBE.Business.Models.Product;
using FidelidadeBE.Tests.API.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace FidelidadeBE.Tests.API;

public class AccessControllerTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestWebApplicationFactory<Program> _factory;

    public AccessControllerTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            BaseAddress = new Uri("https://localhost:7125/api", UriKind.Absolute)
        });
    }

    [Fact]
    public async Task LoginNotSuccessfully()
    {
        //Arrange
        var userAccess = new UserAccessRequestModel()
        {
            Email = "administratorx@application.com",
            Password = "P@ssw0rd",
        };

        var expectedReturn = new ErrorVM(new List<string>() {"Login doesn't exist"});

        //Act
        var requestResult = await _client.PostAsJsonAsync(
            "/v1.0/Access/Login",
            userAccess
        );

        var response = JsonConvert.DeserializeObject<ErrorVM>(await requestResult.Content.ReadAsStringAsync());
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, requestResult.StatusCode);
        Assert.Equal(JsonConvert.SerializeObject(expectedReturn), JsonConvert.SerializeObject(response));
    }
}