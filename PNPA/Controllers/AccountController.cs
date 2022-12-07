using Microsoft.AspNetCore.Mvc;

namespace PNPA.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
