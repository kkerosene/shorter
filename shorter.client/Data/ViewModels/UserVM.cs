namespace shorter.client.Data.ViewModels
{
    public class UserVM
    {
        public string Email { get; set; }
        public int LinkCount { get; set; }
        public int TotalClicks { get; set; }
        public List<UrlVM>? Urls { get; set; }
    }
}