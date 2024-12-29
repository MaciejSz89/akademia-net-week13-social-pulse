using SocialPulse.Areas.Identity.Data;

namespace SocialPulse.Core.Models
{
    public class SocialProfile
    {
        public int Id { get; set; }
        public string SocialPulseUserId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public byte[] ProfileImage { get; set; } = null!;
        public List<UserLink> UserLinks { get; set; } = new List<UserLink>();
        public List<SocialLink> SocialLinks { get; set; } = new List<SocialLink>();
        public SocialPulseUser? SocialPulseUser { get; set; }
    }
}
