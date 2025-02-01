using AutoMapper;
using SocialPulse.Core.Services;
using SocialPulse.Core.ViewModels;
using System.Security.Claims;

namespace SocialPulse.Persistence.Services
{
    public class SocialProfileService : ISocialProfileService
    {
        private readonly ISocialNetworkService _socialNetworkService;
        private readonly IMapper _mapper;

        public SocialProfileService(IServiceProvider serviceProvider)
        {
            _socialNetworkService = serviceProvider.GetRequiredService<ISocialNetworkService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }
        public async Task<SocialProfileViewModel?> CreateSocialProfileViewModelAsync(ClaimsPrincipal user)
        {
            var socialNetworks = _mapper.Map<List<SocialNetworkViewModel>>(await _socialNetworkService.GetAsync());
            List<SocialLinkViewModel> links = new List<SocialLinkViewModel>();

            foreach (var socialNetwork in socialNetworks)
            {
                links.Add(new SocialLinkViewModel
                {
                    SocialNetworkId = socialNetwork.Id,
                    SocialNetwork = socialNetwork,
                });
            }

            var userName = user.Identity?.Name;
            var email = user.FindFirstValue(ClaimTypes.Email);

            if (email == null || userName == null)
                return null;

            var vm = new SocialProfileViewModel
            {
                Email = email,
                UserName = userName,
                SocialLinks = links
            };

            return vm;
        }
    }
}
