using SocialPulse.Core.Models;

namespace SocialPulse.Core.ViewModels
{
    public class UserLinkViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public byte[]? Image { get; set; }
        public int SocialProfileId { get; set; }
        public SocialProfile? SocialProfile { get; set; }
    }
}
