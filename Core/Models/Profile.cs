namespace SocialPulse.Core.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public byte[] ProfileImage { get; set; } = null!;
        public List<MyLink> MyLinks { get; set; } = new List<MyLink>();
        public List<SocialLink> SocialLinks { get; set; } = new List<SocialLink>();

    }
}
