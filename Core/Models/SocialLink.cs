namespace SocialPulse.Core.Models
{
    public class SocialLink
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Url { get; set; } = null!;
        public bool IsVisible { get; set; }
        public byte[] IconImage { get; set; } = null!;

        public int ProfileId { get; set; }

        public Profile? Profile { get; set; }

    }
}
