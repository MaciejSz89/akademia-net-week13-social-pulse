using SocialPulse.Core.Models;

namespace SocialPulse.Core.ViewModels
{
    public class SocialLinkViewModel
    {
        public int Id { get; set; }
        public string? RemainingUrl { get; set; }
        public bool IsVisible { get; set; }
        public int SocialProfileId { get; set; }
        public int SocialNetworkId { get; set; }
        public SocialProfileViewModel? SocialProfile { get; set; }
        public SocialNetworkViewModel SocialNetwork { get; set; } = null!;
    }
}
