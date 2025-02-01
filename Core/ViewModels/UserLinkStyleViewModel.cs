using SocialPulse.Core.Models;

namespace SocialPulse.Core.ViewModels
{
    public class UserLinkStyleViewModel
    {
        public SocialProfile SocialProfile { get; set; } = null!;
        public List<string> UserLinkStyles { get; set; } = new();

    }
}
