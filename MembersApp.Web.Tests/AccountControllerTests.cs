using MembersApp.Application.Dtos;
using MembersApp.Application.Users;
using MembersApp.Web.Views.Account;
using MembersApp.Web.Views.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MembersApp.Web.Tests;
public class AccountControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly AccountController _accountController;
    public AccountControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _accountController = new AccountController(_userServiceMock.Object);
    }

    [Fact]
    public void Login_Get_ReturnsView()
    {
        //Act
        var result = _accountController.Login();

        //Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Login_Post_ValidViewModel_RedirectsOnSuccess()
    {
        // Arrange
        var viewModel = new LoginVM
        {
            Password = "password",
            Username = "testuser"
        };

        _userServiceMock
            .Setup(user => user.SignInAsync(viewModel.Username, viewModel.Password))
            .ReturnsAsync(new UserResultDto(null));

        // Act
        var result = await _accountController.Login(viewModel);

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(MemberController.Members), redirect.ActionName);
    }

    [Theory]
    [InlineData("", "password")]
    [InlineData("testuser", "")]
    public async Task Login_Post_InvalidViewModel_ReturnsViewWithModel(string userName, string password)
    {
        // Arrange
        _accountController.ModelState.AddModelError("Username", "Required");
        _accountController.ModelState.AddModelError("Password", "Required");

        var viewModel = new LoginVM
        {
            Password = password,
            Username = userName
        };

        // Act
        var result = await _accountController.Login(viewModel);

        // Assert
        var view = Assert.IsType<ViewResult>(result);
        Assert.Null(view.Model);
    }

    [Fact]
    public async Task Login_Post_ValidViewModel_ReturnsViewOnFailure()
    {
        // Arrange
        var viewModel = new LoginVM
        {
            Password = "password",
            Username = "testuser"
        };

        _userServiceMock
            .Setup(user => user.SignInAsync(viewModel.Username, viewModel.Password))
            .ReturnsAsync(new UserResultDto("Invalid credential"));

        // Act
        var result = await _accountController.Login(viewModel);

        //Assert
        var view = Assert.IsType<ViewResult>(result);
        Assert.Null(view.Model);
        Assert.True(_accountController.ModelState.ErrorCount >= 1);
    }

    [Fact]
    public async Task Logout_CallsSignOutAsyncAndRedirectsToLogin()
    {
        // Arrange
        _userServiceMock
            .Setup(user => user.SignOutAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _accountController.Logout();

        // Assert
        _userServiceMock.Verify(user => user.SignOutAsync(), Times.Once);
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(AccountController.Login), redirect.ActionName);
    }

    [Fact]
    public void Register_Get_ReturnsView()
    {
        // Act
        var result = _accountController.Register();
        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Register_Post_ValidViewModel_RedirectsOnSuccess()
    {
        var viewModel = new RegisterVM
        {
            Username = "newuser",
            Password = "password",
            IsAdmin = false
        };

        _userServiceMock
            .Setup(user => user.CreateUserAsync(viewModel.Username, viewModel.Password, viewModel.IsAdmin))
            .ReturnsAsync(new UserResultDto(null));

        var result = await _accountController.Register(viewModel);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(AccountController.Login), redirect.ActionName);
    }

    [Fact]
    public async Task Register_Post_InvalidViewModel_ReturnsViewWithModel()
    {
        _accountController.ModelState.AddModelError("Username", "Required");
        _accountController.ModelState.AddModelError("Password", "Required");
        var viewModel = new RegisterVM
        {
            Username = string.Empty,
            Password = string.Empty
        };

        // Act
        var result = await _accountController.Register(viewModel);

        //Assert
        var view = Assert.IsType<ViewResult>(result);
        Assert.Null(view.Model);
    }

    [Fact]
    public async Task Register_Post_ValidViewModel_ReturnsViewOnFailure()
    {
        // Arrange
        var viewModel = new RegisterVM
        {
            Password = "password",
            Username = "testuser",
            PasswordRepeat = "password",
            IsAdmin = false
        };

        _userServiceMock
            .Setup(user => user.CreateUserAsync(viewModel.Username, viewModel.Password, viewModel.IsAdmin))
            .ReturnsAsync(new UserResultDto("Something went wrong"));

        // Act
        var result = await _accountController.Register(viewModel);

        //Assert
        var view = Assert.IsType<ViewResult>(result);
        Assert.Null(view.Model);
        Assert.True(_accountController.ModelState.ErrorCount >= 1);
    }
}
