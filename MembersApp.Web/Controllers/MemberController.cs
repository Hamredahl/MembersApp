using MembersApp.Application;
using MembersApp.Application.Members.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembersApp.Web.Controllers
{
    public class MemberController(IMemberService service) : Controller
    {

        [Authorize]
        [HttpGet("")]
        [HttpGet("members")]
        public IActionResult Members()
        {
            return View();
        }

        [HttpGet ("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task <IActionResult> Create(CreateVM viewModel)
        {
            var result = await service.AddMemberAsync(viewModel.FirstName, viewModel.LastName, viewModel.Email, viewModel.PhoneNumber),
               

            await return View();


        }
    }
}
