namespace SocialPulse.Core.Models.Domains
{
    public class SocialLink
    {
        public int Id { get; set; }
        public string RemainingUrl { get; set; } = null!;
        public int SocialProfileId { get; set; }
        public int SocialNetworkId { get; set; }
        public SocialProfile SocialProfile { get; set; } = null!;
        public SocialNetwork SocialNetwork { get; set; } = null!;

    }
}
