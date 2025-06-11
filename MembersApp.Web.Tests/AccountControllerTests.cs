using MembersApp.Application.Dtos;
using MembersApp.Application.Users;
using MembersApp.Web.Controllers;
using MembersApp.Web.Views.Account;
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
    public async Task Login_ValidViewModel_RedirectsOnSuccess()
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
    public async Task Login_InvalidViewModel_ReturnsViewWithModel(string userName, string password)
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




}
