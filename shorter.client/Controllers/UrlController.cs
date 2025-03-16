using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shorter.client.Data;
using shorter.client.Data.Models;
using shorter.client.Data.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace shorter.client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UrlController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UrlController> _logger;

        public UrlController(AppDbContext context, ILogger<UrlController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUrls()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var urls = await _context.Urls
                .Where(u => u.UserId == userId)
                .Select(u => new UrlVM
                {
                    Id = u.Id,
                    LongUrl = u.LongUrl,
                    ShortUrl = u.ShortUrl,
                    TotalClicks = u.TotalClicks,
                    DateCreated = u.DateCreated
                })
                .ToListAsync();

            return Ok(urls);
        }

        [HttpGet("RedirectShortUrl")]
        [AllowAnonymous]
        public async Task<IActionResult> RedirectShortUrl(string shortUrl)
        {
            var url = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
            if (url == null)
                return NotFound();

            url.TotalClicks++;
            await _context.SaveChangesAsync();

            return Redirect(url.LongUrl);  // Issues a 302 redirect to the long URL.
        }

        // Removed [ValidateAntiForgeryToken] for API simplicity.
        [HttpPost("CreateUrl")]
        public async Task<IActionResult> CreateUrl([FromBody] UrlCreateRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Url) || !Uri.TryCreate(request.Url, UriKind.Absolute, out _))
                    return new JsonResult(new { error = "Invalid URL format" });

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var existing = await _context.Urls
                    .FirstOrDefaultAsync(u => u.LongUrl == request.Url && u.UserId == userId);

                if (existing != null)
                    return new JsonResult(new { error = "URL already exists" });

                // Generate a unique short code.
                string shortCode = await GenerateUniqueShortUrl();

                // Create the new URL record.
                var newUrl = new Url
                {
                    LongUrl = request.Url,
                    ShortUrl = shortCode,
                    UserId = userId,
                    TotalClicks = 0,
                    DateCreated = DateTime.UtcNow
                };

                _context.Urls.Add(newUrl);
                await _context.SaveChangesAsync();

                return new JsonResult(new UrlVM
                {
                    Id = newUrl.Id,
                    LongUrl = newUrl.LongUrl,
                    ShortUrl = newUrl.ShortUrl,
                    TotalClicks = newUrl.TotalClicks,
                    DateCreated = newUrl.DateCreated
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating URL");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUrl(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            var url = await _context.Urls
                .FirstOrDefaultAsync(u => u.Id == id && (u.UserId == userId || isAdmin));

            if (url == null)
                return NotFound();

            _context.Urls.Remove(url);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<string> GenerateUniqueShortUrl()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string shortUrl;

            do
            {
                shortUrl = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (await _context.Urls.AnyAsync(u => u.ShortUrl == shortUrl));

            return shortUrl;
        }
    }
}
