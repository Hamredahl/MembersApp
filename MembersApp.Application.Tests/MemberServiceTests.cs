using MembersApp.Application.Members.Interfaces;
using MembersApp.Application.Members.Services;
using MembersApp.Domain.Entities;
using Moq;

namespace MembersApp.Application.Tests;

public class MemberServiceTests
{
    [Fact]
    public async Task AddMemberAsync_ShouldAddANewMemberToRepository()
    {
        //Arrange
        var mockRepository = new Mock<IMemberRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.Members).Returns(mockRepository.Object);

        var memberService = new MemberService(mockUnitOfWork.Object);

        //Act 
        var newMember = new Member { Name = "john doe", Email = "Test@test.com", };
        await memberService.AddMemberAsync(newMember);

        //Assert
        mockRepository.Verify(m => m.AddMemberAsync(It.Is<Member>(m => m.Name == "John Doe" && m.Email == "test@test.com")), Times.Exactly(1));
    }

    [Fact]

    public async Task GetAllMembersAsync__ShouldReturnAllMembersOrderedByName()
    {
        //Arrange
        var members = new[]
        {
            new Member { Name = "Test 3", Email ="test3@test.com" },
            new Member { Name = "Test 2", Email ="test2@test.com" },
            new Member { Name = "Test 4", Email ="test4@test.com" },
            new Member { Name = "Test 1", Email ="test1@test.com" },
            new Member { Name = "Test 5", Email ="test5@test.com" },
        };
        var sortedMembers = members.OrderBy(m => m.Name).ToArray();

        var mockRepository = new Mock<IMemberRepository>();
        mockRepository.Setup(m => m.GetAllMembersAsync()).ReturnsAsync(members);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.Members).Returns(mockRepository.Object);

        var memberService = new MemberService(mockUnitOfWork.Object);

        //Act
        var serviceMembers = await memberService.GetAllMembersAsync();

        //Assert
        Assert.Equal(sortedMembers, serviceMembers);
        Assert.NotEmpty(serviceMembers);
    }


}
