namespace SocialPulse.Core.ViewModels;

public class SocialLinkViewModel
{
    public int Id { get; set; }
    public string? RemainingUrl { get; set; }
    public bool IsVisible => !string.IsNullOrWhiteSpace(RemainingUrl);
    public int SocialProfileId { get; set; }
    public int SocialNetworkId { get; set; }
    public SocialProfileViewModel? SocialProfile { get; set; }
    public SocialNetworkViewModel SocialNetwork { get; set; } = null!;
}
