namespace SocialPulse.Core.Models
{
    public class UserLinkStyle
    {
        public int Id { get; set; }
        public int SocialProfileId { get; set; } 
        public string Style { get; set; } = null!;

        public SocialProfile? SocialProfile { get; set; }
    }
}
