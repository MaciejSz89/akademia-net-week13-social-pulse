using SocialPulse.Areas.Identity.Data;
using SocialPulse.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace SocialPulse.Core.ViewModels
{
    public class SocialProfileViewModel
    {

        private const string DefaultProfileImagePath = "/images/default-profile-image.webp";
        private byte[]? _profileImage;

        public int Id { get; set; }
        public string SocialPulseUserId { get; set; } = null!;


        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Pole Opis jest wymagane.")]
        public string Content { get; set; } = null!;
        public byte[] ProfileImage
        {
            get
            {
                if (_profileImage == null || _profileImage.Length == 0)
                {
                    string defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", DefaultProfileImagePath.TrimStart('/'));
                    if (File.Exists(defaultImagePath))
                    {
                        _profileImage = File.ReadAllBytes(defaultImagePath);
                    }
                }

                return _profileImage!;
            }
            set
            {
                _profileImage = value;
            }
        }
        public string ProfileImageBase64 => $"data:image/jpeg;base64,{Convert.ToBase64String(ProfileImage)}";
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
