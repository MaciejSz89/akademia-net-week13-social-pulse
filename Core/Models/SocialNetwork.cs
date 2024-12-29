using System.Drawing;

namespace SocialPulse.Core.Models
{
    public class SocialNetwork
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string BaseDomain { get; set; } = null!;
        public byte[] Icon { get; set; } = null!;
    }
}
