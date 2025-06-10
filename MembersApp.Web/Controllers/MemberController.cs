using Microsoft.AspNetCore.Mvc;

namespace MembersApp.Web.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
