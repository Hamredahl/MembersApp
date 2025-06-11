using MembersApp.Application.Members.Interfaces;
using MembersApp.Domain.Entities;
using MembersApp.Web.Controllers;
using MembersApp.Web.Views.Member;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MembersApp.Web.Tests;

public class MemberControllerTests
{
    [Fact]
    public async Task Members_NoParameters_ReturnsViewResultWithMembersViewModel()
    {
        // Arrange
        var members = new[]
        {
            new Member { Name = "Alice", MemberAddress = new Address { City = "Örebro" } },
            new Member { Name = "Anna" },
            new Member { Name = "Bob", MemberAddress = new Address { City = "Stockholm" } }
        };
        var mockService = new Mock<IMemberService>();
        mockService.Setup(m => m.GetAllMembersAsync()).ReturnsAsync(members);

        var controller = new MemberController(mockService.Object);

        // Act
        var result = await controller.Members();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<MembersVM>(viewResult.Model);
    }

    [Fact]
    public async Task Create_ValidViewModel_RedirectsToMembers()
    {
        var mockService = new Mock<IMemberService>();
        var controller = new MemberController(mockService.Object);
        var viewModel = new CreateVM
        {
            Name = "John Doe",
            Email = "jhon@doe.se",
            City = "Örebro",
            Phone = "123456789",
            Street = "Testgatan 1",
            ZipNumber = "12345"
        };

        // Act
        var result = await controller.Create(viewModel);

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Members", redirect.ActionName);
        mockService.Verify(m => m.AddMemberAsync(It.IsAny<Member>()), Times.Once);
    }

    [Theory]
    [InlineData("", "jhon@doe.se", "123456789")]
    [InlineData("Jhon Doe", "jhondoe#se", "123456789")]
    [InlineData("Jhon Doe", "jhon@doe.se", "r123456789")]
    public async Task Create_InvalidViewModel_ReturnsCreateView(string name, string email, string phone)
    {
        var mockService = new Mock<IMemberService>();
        var controller = new MemberController(mockService.Object);
        controller.ModelState.AddModelError("Name", "Name is required");
        controller.ModelState.AddModelError("Email", "Email is not valid");
        controller.ModelState.AddModelError("Phone", "Phone number is not valid");

        var viewModel = new CreateVM
        {
            Name = name,
            Email = email,
            Phone = phone,
            City = "Örebro",
            Street = "Testgatan 1",
            ZipNumber = "12345"
        };

        // Act
        var result = await controller.Create(viewModel);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.Model);
        mockService.Verify(m => m.AddMemberAsync(It.IsAny<Member>()), Times.Never);
    }

    [Fact]
    public void Create_Get_ReturnsViewResult()
    {
        // Arrange
        var mockService = new Mock<IMemberService>();
        var controller = new MemberController(mockService.Object);

        // Act
        var result = controller.Create();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.Model);
    }
}
