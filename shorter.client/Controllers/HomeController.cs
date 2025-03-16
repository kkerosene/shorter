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

    public IActionResult Index() => View(new UrlVM()
    {
        Id = 0,
        LongUrl = "", // Initialize required properties with default values
        ShortUrl = "",
        TotalClicks = 0,
        DateCreated = DateTime.UtcNow
    });

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Shorten()
    {
        return RedirectToAction("Index");
    }
}
