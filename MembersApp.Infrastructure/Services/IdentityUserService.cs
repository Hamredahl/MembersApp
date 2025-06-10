using MembersApp.Application.Dtos;
using MembersApp.Application.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Infrastructure.Services;

public class IdentityUserService(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    RoleManager<IdentityRole> roleManager) : IIdentityUserService
{
    public async Task<UserResultDto> CreateUserAsync(string userName, string password, bool isAdmin)
    {
        var result = await userManager.CreateAsync(new IdentityUser
        {
            UserName = userName,
            //Email = userName
        }, password);

        if (result.Succeeded && isAdmin)
        {
            string role = "Administrators";
            if (!await roleManager.RoleExistsAsync(role)) await roleManager.CreateAsync(new IdentityRole(role));
            await userManager.AddToRoleAsync(await userManager.FindByNameAsync(userName), role);
        }  
        return new UserResultDto(result.Errors.FirstOrDefault()?.Description);
    }

    public async Task<UserResultDto> SignInAsync(string userName, string password)
    {
        var result = await signInManager.PasswordSignInAsync(userName, password, false, false);
        return new UserResultDto(result.Succeeded ? null : "Invalid user credentials");
    }
    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }
}
