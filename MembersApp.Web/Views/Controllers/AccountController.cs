using MembersApp.Application.Users;
using MembersApp.Domain.Entities;
using MembersApp.Web.Views.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembersApp.Web.Views.Controllers
{
    public class AccountController(IUserService userService) : Controller
    {
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterVM viewModel)
        {
            if (!ModelState.IsValid)
                return View();
            
            var result = await userService.CreateUserAsync(viewModel.Username, viewModel.Password, viewModel.IsAdmin);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage!);
                return View();
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await userService.SignInAsync(viewModel.Username, viewModel.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage!);
                return View();
            }

            return RedirectToAction(nameof(MemberController.Members), nameof(Member));
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
