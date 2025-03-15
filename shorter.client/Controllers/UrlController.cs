using Microsoft.AspNetCore.Mvc;
using shorter.client.Data.Models;
using shorter.client.Data.ViewModels;

namespace shorter.client.Controllers
{
    public class UrlController : Controller
    {
        public IActionResult Index()
        {
            var allUrls = new List<GetUrlVM>()
            {
                new GetUrlVM()
                {
                    Id = 1,
                    OriginalLink = "https://link1.com",
                    ShortLink = "lman4",
                    TotalClicks = 7,
                    UserId = 3,
                },
                new GetUrlVM()
                {
                    Id = 2,
                    OriginalLink = "https://link2.com",
                    ShortLink = "j7tf",
                    TotalClicks = 0,
                    UserId = 1,
                },
                new GetUrlVM()
                {
                    Id = 3,
                    OriginalLink = "https://link3.com",
                    ShortLink = "ku2e",
                    TotalClicks = 16,
                    UserId = 3,
                }
            };


            return View(allUrls);
        }

        public IActionResult Create()
        {
            /*
                <button type="button" class="btn btn-primary" id="liveToastBtn">Show live toast</button>

                <div class="toast-container position-fixed bottom-0 end-0 p-3">
                  <div id="liveToast" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
                    <div class="toast-header">
                      <img src="..." class="rounded me-2" alt="...">
                      <strong class="me-auto">Bootstrap</strong>
                      <small>11 mins ago</small>
                      <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                    <div class="toast-body">
                      Hello, world! This is a toast message.
                    </div>
                  </div>
                </div>

             */

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            return View();
        }
        public IActionResult Remove(int userId, int linkId)
        {
            return View();
        }
    }
}
