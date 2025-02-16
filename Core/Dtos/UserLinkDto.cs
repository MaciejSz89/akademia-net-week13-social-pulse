namespace SocialPulse.Core.Dtos
{
    public class UserLinkDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public IFormFile? Image { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
