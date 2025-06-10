using MembersApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Application.Users
{
    public interface IUserService
    {
        Task<UserResultDto> CreateUserAsync(string userName, string password);
        Task<UserResultDto> SignInAsync(string userName, string password);
        Task SignOutAsync();
    }
}
