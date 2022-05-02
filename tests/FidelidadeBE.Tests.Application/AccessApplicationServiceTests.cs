using System.Collections.Generic;
using System.Threading.Tasks;
using FidelidadeBE.Application.Services;
using FidelidadeBE.Business.Interfaces;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using FidelidadeBE.Data.Interfaces;
using FidelidadeBE.Tests.Application.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Enumerable = System.Linq.Enumerable;

namespace FidelidadeBE.Tests.Application;

public class AccessApplicationServiceTests
{
    [Fact]
    public async Task LoginError_WhenUserDoesntExist()
    {
        // Arrange
        var userManagerMock = MockConfiguration.MockUserManager<IdentityUser>();
        
        userManagerMock.Setup(x =>
                x.FindByEmailAsync(It.IsAny<string>()))
            .Returns(async () =>
            {
                await Task.CompletedTask;
                return null;
            });
        
        var roleManagerMock = MockConfiguration.MockRoleManager<IdentityRole>();
        var notificatorMock = new Mock<INotificator>();
        
        var notificationList = new List<Notification>();

        notificatorMock.Setup(x =>
                x.AddNotification(It.IsAny<string>(), It.IsAny<NotificationType>()))
            .Callback((string addedNotification, NotificationType addedNotificationType) => 
                notificationList.Add(new Notification(addedNotification, addedNotificationType)));
        
        var identityRepositoryMock = new Mock<IIdentityRepository>();
        var signInManagerMock = MockConfiguration.MockSignInManager(userManagerMock);
        var aspNetUserMock = new Mock<IUser>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var clientRepositoryMock = new Mock<IClientRepository>();
        var companyRepositoryMock = new Mock<ICompanyRepository>();

        // Act
        var identityApplicationService = new IdentityApplicationService(
            userManagerMock.Object,
            roleManagerMock.Object,
            notificatorMock.Object,
            identityRepositoryMock.Object,
            signInManagerMock.Object,
            aspNetUserMock.Object,
            userRepositoryMock.Object,
            clientRepositoryMock.Object,
            companyRepositoryMock.Object
        );

        var result = await identityApplicationService.LoginAsync("Teste", "Teste");
        
        //Assert
        var expectedResult = false;
        var expectedNotification = new Notification("Login doesn't exist", NotificationType.IncorrectData);
        var notificationAmmount = 1;
        
        Assert.Equal(expectedResult, result);
        Assert.Equal(JsonConvert.SerializeObject(expectedNotification), JsonConvert.SerializeObject(Enumerable.First(notificationList)));
        Assert.Equal(notificationAmmount, notificationList.Count);
    }
    
    [Fact]
    public async Task LoginError_WhenUserOrPasswordIsIncorrect()
    {
        // Arrange
        var userManagerMock = MockConfiguration.MockUserManager<IdentityUser>();
        
        userManagerMock.Setup(x =>
                x.FindByEmailAsync(It.IsAny<string>()))
            .Returns(async () =>
            {
                await Task.CompletedTask;
                return new IdentityUser();
            });
        
        var roleManagerMock = MockConfiguration.MockRoleManager<IdentityRole>();
        var notificatorMock = new Mock<INotificator>();
        
        var notificationList = new List<Notification>();

        notificatorMock.Setup(x =>
                x.AddNotification(It.IsAny<string>(), It.IsAny<NotificationType>()))
            .Callback((string addedNotification, NotificationType addedNotificationType) => 
                notificationList.Add(new Notification(addedNotification, addedNotificationType)));
        
        var identityRepositoryMock = new Mock<IIdentityRepository>();
        var signInManagerMock = MockConfiguration.MockSignInManager(userManagerMock);

        signInManagerMock.Setup(x =>
                x.PasswordSignInAsync(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .Returns(async () =>
            {
                await Task.CompletedTask;
                return SignInResult.Failed;
            });
        
        var aspNetUserMock = new Mock<IUser>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var clientRepositoryMock = new Mock<IClientRepository>();
        var companyRepositoryMock = new Mock<ICompanyRepository>();

        // Act
        var identityApplicationService = new IdentityApplicationService(
            userManagerMock.Object,
            roleManagerMock.Object,
            notificatorMock.Object,
            identityRepositoryMock.Object,
            signInManagerMock.Object,
            aspNetUserMock.Object,
            userRepositoryMock.Object,
            clientRepositoryMock.Object,
            companyRepositoryMock.Object
        );

        var result = await identityApplicationService.LoginAsync("Teste", "Teste");
        
        //Assert
        var expectedResult = false;
        var expectedNotification = new Notification("Username or Password incorrect", NotificationType.IncorrectData);
        var notificationAmmount = 1;
        
        Assert.Equal(expectedResult, result);
        Assert.Equal(JsonConvert.SerializeObject(expectedNotification), JsonConvert.SerializeObject(Enumerable.First(notificationList)));
        Assert.Equal(notificationAmmount, notificationList.Count);
    }
    
    [Fact]
    public async Task LoginSuccess()
    {
        // Arrange
        var userManagerMock = MockConfiguration.MockUserManager<IdentityUser>();
        
        userManagerMock.Setup(x =>
                x.FindByEmailAsync(It.IsAny<string>()))
            .Returns(async () =>
            {
                await Task.CompletedTask;
                return new IdentityUser();
            });
        
        var roleManagerMock = MockConfiguration.MockRoleManager<IdentityRole>();
        var notificatorMock = new Mock<INotificator>();
        
        var notificationList = new List<Notification>();

        notificatorMock.Setup(x =>
                x.AddNotification(It.IsAny<string>(), It.IsAny<NotificationType>()))
            .Callback((string addedNotification, NotificationType addedNotificationType) => 
                notificationList.Add(new Notification(addedNotification, addedNotificationType)));
        
        var identityRepositoryMock = new Mock<IIdentityRepository>();
        var signInManagerMock = MockConfiguration.MockSignInManager(userManagerMock);

        signInManagerMock.Setup(x =>
                x.PasswordSignInAsync(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .Returns(async () =>
            {
                await Task.CompletedTask;
                return SignInResult.Success;
            });
        
        var aspNetUserMock = new Mock<IUser>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var clientRepositoryMock = new Mock<IClientRepository>();
        var companyRepositoryMock = new Mock<ICompanyRepository>();

        // Act
        var identityApplicationService = new IdentityApplicationService(
            userManagerMock.Object,
            roleManagerMock.Object,
            notificatorMock.Object,
            identityRepositoryMock.Object,
            signInManagerMock.Object,
            aspNetUserMock.Object,
            userRepositoryMock.Object,
            clientRepositoryMock.Object,
            companyRepositoryMock.Object
        );

        var result = await identityApplicationService.LoginAsync("Teste", "Teste");
        
        //Assert
        var expectedResult = true;
        var notificationAmmount = 0;
        
        Assert.Equal(expectedResult, result);
        Assert.Equal(notificationAmmount, notificationList.Count);
    }
}