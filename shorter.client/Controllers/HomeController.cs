using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using shorter.client.Data.ViewModels;

namespace shorter.client.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var newUrl = new PostUrlVM();

        return View(newUrl);
    }

    public IActionResult Shorten()
    {
        return RedirectToAction("Index");
    }
}
