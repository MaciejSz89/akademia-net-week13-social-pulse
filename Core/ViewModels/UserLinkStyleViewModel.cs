using SocialPulse.Core.Models.Domains;

namespace SocialPulse.Core.ViewModels;

public class UserLinkStyleViewModel
{
    public string UserName { get; set; } = null!;
    public string CurrentUserLinkStyle { get; set; } = null!;
    public List<string> UserLinkStyles { get; set; } = new();
}
