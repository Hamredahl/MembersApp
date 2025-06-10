using MembersApp.Application;
using MembersApp.Application.Members.Interfaces;
using MembersApp.Domain.Entities;
using MembersApp.Web.Views.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MembersApp.Web.Controllers
{
    public class MemberController(IMemberService service) : Controller
    {

        [Authorize]
        [HttpGet("")]
        [HttpGet("members")]
        public async Task<IActionResult> Members()
        {
            var model = await service.GetAllMembersAsync();
            var viewModel = new MembersVM()
            {
                MembersVMs = [.. model.Select(m => new MembersVM.MemberVM()
                {
                    Name = m.Name,
                    City = m.MemberAddress?.City
                })]
            };
            return View(viewModel);
        }

        [Authorize(Roles = "Administrators")]
        [HttpGet ("create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrators")]
        [HttpPost("create")]
        public async Task <IActionResult> Create(CreateVM viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            Address address = new() { Street = viewModel.Street, ZipNumber = viewModel.ZipNumber, City = viewModel.City};
            Member member = new() { Name = viewModel.Name, Email = viewModel.Email, Phone = viewModel.Phone, MemberAddress = address, };
            await service.AddMemberAsync(member);  

            return RedirectToAction(nameof(Members));
        }
    }
}
