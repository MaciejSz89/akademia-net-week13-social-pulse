using SocialPulse.Core.Models.Domains;

namespace SocialPulse.Core.ViewModels;

public class UserLinkViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public byte[]? Image { get; set; }
    public string? ImageBase64 => Image is not null ? $"data:image/jpeg;base64,{Convert.ToBase64String(Image)}" : null;
    public int SocialProfileId { get; set; }
    public SocialProfile? SocialProfile { get; set; }
}
