using System.ComponentModel.DataAnnotations;

namespace shorter.client.Data.ViewModels
{
    public class UrlVM
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public int TotalClicks { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class UrlCreateRequest
    {
        [Required]
        [Url]
        public string Url { get; set; }

        [StringLength(10, MinimumLength = 3,
         ErrorMessage = "short URL must be between 3-10 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$",
         ErrorMessage = "only letters, numbers, underscores and hyphens allowed")]
        public string? ShortUrl { get; set; }
    }
}
