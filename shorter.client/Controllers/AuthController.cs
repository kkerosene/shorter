using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shorter.client.Data;
using shorter.client.Data.Models;
using shorter.client.Data.ViewModels;
using System.Security.Claims;

namespace shorter.client.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AppDbContext context) // Inject AppDbContext
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVM
            {
                Username = "",
                Email = "",
                Password = "",
                ConfirmPassword = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterSubmit(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Json(new { success = true, redirectUrl = "/" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVM
            {
                Email = "",
                Password = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> LoginSubmit(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false, error = "Invalid login attempt." });
            }

            return Json(new { success = false, error = "Invalid data." });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users
                .Include(u => u.Urls) // Include related URLs
                .Select(u => new UserVM
                {
                    Email = u.Email,
                    LinkCount = u.Urls.Count,
                    TotalClicks = u.Urls.Sum(url => url.TotalClicks),
                    Urls = u.Urls.Select(url => new UrlVM
                    {
                        Id = url.Id,
                        LongUrl = url.LongUrl,
                        ShortUrl = url.ShortUrl,
                        TotalClicks = url.TotalClicks,
                        DateCreated = url.DateCreated
                    }).ToList()
                })
                .ToListAsync();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}