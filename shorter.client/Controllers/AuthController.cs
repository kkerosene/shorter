using Microsoft.AspNetCore.Mvc;
using shorter.client.Data.ViewModels;

namespace shorter.client.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View(new LoginVM());
        }
        public IActionResult LoginSubmit(LoginVM loginVM)
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
