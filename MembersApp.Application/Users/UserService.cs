﻿using MembersApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Application.Users;

public class UserService(IIdentityUserService identityUserService) : IUserService
{
    public async Task<UserResultDto> CreateUserAsync(string userName, string password, bool isAdmin) =>
        await identityUserService.CreateUserAsync(userName, password, isAdmin);

    public async Task<UserResultDto> SignInAsync(string userName, string password) =>
        await identityUserService.SignInAsync(userName, password);

    public async Task SignOutAsync()
    {
        await identityUserService.SignOutAsync();
    }
}
