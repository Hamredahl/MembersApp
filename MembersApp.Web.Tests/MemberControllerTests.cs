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

    }
}
