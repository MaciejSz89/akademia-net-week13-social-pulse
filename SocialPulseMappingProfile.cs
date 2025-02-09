using AutoMapper;
using SocialPulse.Core.Dtos;
using SocialPulse.Core.Models;
using SocialPulse.Core.ViewModels;
using SocialPulse.Helpers;

namespace SocialPulse
{
    public class SocialPulseMappingProfile : Profile
    {
        public SocialPulseMappingProfile()
        {
            CreateMap<SocialProfile, SocialProfileViewModel>();
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
            CreateMap<UserLink, UserLinkViewModel>();
            CreateMap<UserLinkViewModel, UserLink>();
            CreateMap<UserLinkDto, UserLinkViewModel>();
            CreateMap<UserLinkViewModel, UserLinkDto>();
        }
    }
}
