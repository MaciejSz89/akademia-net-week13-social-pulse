using AutoMapper;
using SocialPulse.Core.Dtos;
using SocialPulse.Core.Models;
using SocialPulse.Core.ViewModels;

namespace SocialPulse
{
    public class SocialPulseMappingProfile : Profile
    {
        public SocialPulseMappingProfile()
        {
            CreateMap<SocialProfile, SocialProfileViewModel>();
            CreateMap<SocialProfileViewModel, SocialProfile>();
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
