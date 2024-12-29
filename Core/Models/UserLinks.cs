using System.Drawing;

namespace SocialPulse.Core.Models
{
    public class UserLinks
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public byte[]? Image { get; set; }
        public int SocialProfileId { get; set; }
        public SocialProfile? SocialProfile { get; set; }

    }
}
