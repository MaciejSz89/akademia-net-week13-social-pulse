namespace SocialPulse.Core.Models
{
    public class SocialLink
    {
        public int Id { get; set; }
        public string RemainingUrl { get; set; } = null!;
        public int SocialProfileId { get; set; }
        public int SocialNetworkId { get; set; }
        public SocialProfile? SocialProfile { get; set; }
        public SocialNetwork? SocialNetwork { get; set; }

    }
}
