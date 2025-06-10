using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembersApp.Web.Controllers
{
    public class MemberController : Controller
    {

        [Authorize]
        [HttpGet("")]
        [HttpGet("members")]
        public IActionResult Members()
        {
            return View();
        }

        
        public IActionResult Index()
        {
            return View();
        }
    }
}
