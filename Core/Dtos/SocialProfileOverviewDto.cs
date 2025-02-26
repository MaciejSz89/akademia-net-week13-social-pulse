using System.ComponentModel.DataAnnotations;

namespace SocialPulse.Core.Dtos
{
    public class SocialProfileOverviewDto
    {
        public int Id { get; set; }

        public string? ProfileImageBase64 { get; set; }

        public string UserName { get; set; } = null!;

        public string Content { get; set; } = null!;

    }
}
