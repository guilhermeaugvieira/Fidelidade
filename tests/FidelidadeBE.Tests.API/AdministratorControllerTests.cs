using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using FidelidadeBE.Business.Models.Base;
using FidelidadeBE.Business.Models.Product;
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
    public async Task AddAdministratorSuccessfully()
    { 
        // Arrange
        var webApplicationFactory = new TestWebApplicationFactory<Program>("AddAdministratorSuccessfully");
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

        var addAdministratorResponse =
            JsonConvert.DeserializeObject<SuccessVM<AddUserResponseModel>>(await addAdministratorResult.Content.ReadAsStringAsync());
        
        _output.WriteLine($"Administrator Created: {JsonConvert.SerializeObject(addAdministratorResponse.Data)}");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, addAdministratorResult.StatusCode);
        Assert.NotNull(addAdministratorResponse!.Data);
    }
    
    [Fact]
    public async Task AddProductSuccessully()
    { 
        // Arrange
        var webApplicationFactory = new TestWebApplicationFactory<Program>("AddProductSuccessully");
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

        var addAdministratorResponse =
            JsonConvert.DeserializeObject<SuccessVM<AddUserResponseModel>>(await addAdministratorResult.Content.ReadAsStringAsync());
        
        _output.WriteLine($"Administrator Created: {JsonConvert.SerializeObject(addAdministratorResponse.Data)}");

        var newProduct = new AddProductRequestModel()
        {
            Name = "Produto Teste 1",
            Points = 5000,
            CategoryPath = "Teste/Produtos/"
        };

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", addAdministratorResponse.Data.AccessToken);
        var addProductResult = await client.PostAsJsonAsync(
            "/v1.0/Administrator/Product",
            newProduct
        );
        
        var addProductResponse = JsonConvert.DeserializeObject<SuccessVM<AddProductResponseModel>>(await addProductResult.Content.ReadAsStringAsync());
        
        _output.WriteLine($"Product Created: {JsonConvert.SerializeObject(addProductResponse.Data)}");

        
        // Assert
        Assert.Equal(HttpStatusCode.OK, addProductResult.StatusCode);
        Assert.NotNull(addProductResponse!.Data);
    }
    
    [Fact]
    public async Task GetAvailableRedeemProductsSuccessully()
    { 
        // Arrange
        var webApplicationFactory = new TestWebApplicationFactory<Program>("GetAvailableRedeemProductsSuccessully");
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

        var addAdministratorResponse =
            JsonConvert.DeserializeObject<SuccessVM<AddUserResponseModel>>(await addAdministratorResult.Content.ReadAsStringAsync());
        
        _output.WriteLine($"Administrator Created: {JsonConvert.SerializeObject(addAdministratorResponse.Data)}");

        var newProduct = new AddProductRequestModel()
        {
            Name = "Produto Teste 1",
            Points = 5000,
            CategoryPath = "Teste/Produtos/"
        };

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", addAdministratorResponse.Data.AccessToken);
        
        var addProductResult = await client.PostAsJsonAsync(
            "/v1.0/Administrator/Product",
            newProduct
        );
        
        var addProductResponse = JsonConvert.DeserializeObject<SuccessVM<AddProductResponseModel>>(await addProductResult.Content.ReadAsStringAsync());
        
        _output.WriteLine($"Product Created: {JsonConvert.SerializeObject(addProductResponse.Data)}");
        
        var redeemProductsResult = await client.GetAsync(
            "/v1.0/Administrator/Product/Available"
        );
        
        var redeemProductResponse = JsonConvert.DeserializeObject<SuccessVM<IEnumerable<AddProductResponseModel>>>(await redeemProductsResult.Content.ReadAsStringAsync());
        
        _output.WriteLine($"Products Available: {JsonConvert.SerializeObject(redeemProductResponse.Data)}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, redeemProductsResult.StatusCode);
        Assert.NotNull(redeemProductResponse!.Data);
    }
}