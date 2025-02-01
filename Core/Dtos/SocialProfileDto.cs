using System.ComponentModel.DataAnnotations;

namespace SocialPulse.Core.Dtos
{
    public class SocialProfileDto
    {
        public int Id { get; set; }

        public IFormFile? ProfileImage { get; set; }

        public string UserName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? Content { get; set; }

        public List<SocialLinkDto> SocialLinks { get; set; } = new List<SocialLinkDto>();
    }
}
