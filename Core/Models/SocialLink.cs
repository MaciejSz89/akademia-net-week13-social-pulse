namespace SocialPulse.Core.Models
{
    public class SocialLink
    {
        public int Id { get; set; }
        public string? Url { get; set; } 
        public bool IsVisible { get; set; }
        public int SocialProfileId { get; set; }
        public int SocialNetworkId { get; set; }
        public SocialProfile? SocialProfile { get; set; }
        public SocialNetwork? SocialNetwork { get; set; }

    }
}
