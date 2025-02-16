using AutoMapper;
using SocialPulse.Core.Dtos;
using SocialPulse.Core.Models.Domains;
using SocialPulse.Core.ViewModels;
using SocialPulse.Helpers;

namespace SocialPulse
{
    public class SocialPulseMappingProfile : Profile
    {
        public SocialPulseMappingProfile()
        {
            CreateMap<SocialProfile, SocialProfileViewModel>();
            CreateMap<UserLink, UserLinkViewModel>();
            CreateMap<SocialProfileViewModel, SocialProfile>();
            CreateMap<SocialProfileDto, SocialProfile>()
                        .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src =>
                            src.ProfileImage != null
                                ? src.ProfileImage.ToByteArray()
                                : Array.Empty<byte>()
                        ))
                        .ForMember(dest => dest.SocialLinks, opt => opt.MapFrom(src =>
                            src.SocialLinks
                                .Where(link => !string.IsNullOrWhiteSpace(link.RemainingUrl))
                        ));

            CreateMap<SocialLinkDto, SocialLink>();
            CreateMap<SocialLink, SocialLinkViewModel>();
            CreateMap<SocialLinkViewModel, SocialLink>();
            CreateMap<SocialNetwork, SocialNetworkViewModel>();
            CreateMap<SocialNetworkViewModel, SocialNetwork>();
            CreateMap<UserLink, UserLinkViewModel>()
                        .ForMember(dest => dest.Image, opt => opt.AllowNull());
            CreateMap<UserLinkDto, UserLink>()
                        .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? ConvertIFormFileToByteArray(src.Image) : ConvertBase64ToByteArray(src.ImageBase64)))
                        .ForMember(dest => dest.Image, opt => opt.AllowNull());
        }

        private static byte[]? ConvertBase64ToByteArray(string? base64String)
        {
            return string.IsNullOrEmpty(base64String) ? null : Convert.FromBase64String(base64String);
        }

        private static byte[]? ConvertIFormFileToByteArray(IFormFile? file)
        {
            if (file == null) return null;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
