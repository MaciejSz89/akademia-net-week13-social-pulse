using SocialPulse.Areas.Identity.Data;
using SocialPulse.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialPulse.Core.ViewModels
{
    public class SocialProfileViewModel
    {
        public int Id { get; set; }
        public string SocialPulseUserId { get; set; } = null!;


        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Pole Opis jest wymagane.")]
        public string Content { get; set; } = null!;
        public byte[] ProfileImage { get; set; } = null!;
        public List<UserLinkViewModel> UserLinks { get; set; } = new List<UserLinkViewModel>();
        public List<SocialLinkViewModel> SocialLinks { get; set; } = new List<SocialLinkViewModel>();

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Pole Email jest wymagane.")]
        public string Email { get; set; } = null!;


        [Display(Name = "Nazwa profilu")]
        [Required(ErrorMessage = "Pole Nazwa profilu jest wymagane.")]
        public string UserName { get; set; } = null!;
    }
}
