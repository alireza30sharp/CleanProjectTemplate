using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult login()
        {
            return View();
        }
    }
}
