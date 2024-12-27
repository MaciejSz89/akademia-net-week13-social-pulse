namespace SocialPulse.Core.Models
{
    public class MyLink
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int ProfileId { get; set; }
        public Profile? Profile { get; set; }

    }
}
