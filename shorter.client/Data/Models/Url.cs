using shorter.client.Data.Models;

public class Url
{
    public int Id { get; set; }
    public string LongUrl { get; set; }
    public string ShortUrl { get; set; }
    public string UserId { get; set; }
    public int TotalClicks { get; set; }
    public DateTime DateCreated { get; set; }

    // Navigation property to ApplicationUser
    public ApplicationUser User { get; set; }
}