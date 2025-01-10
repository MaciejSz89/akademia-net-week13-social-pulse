using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialPulse.Core.Services;
using SocialPulse.Core.ViewModels;

namespace SocialPulse.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISocialNetworkService _socialNetworkService;
        private readonly IMapper _mapper;

        public SettingsController(IServiceProvider serviceProvider)
        {
            _socialNetworkService = serviceProvider.GetRequiredService<ISocialNetworkService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public IActionResult Profile()
        {
            var socialNetworks = _mapper.Map<List<SocialNetworkViewModel>>(_socialNetworkService.GetAsync());
            List<SocialLinkViewModel> links = new List<SocialLinkViewModel>();

            foreach (var socialNetwork in socialNetworks)
            {
                links.Add(new SocialLinkViewModel
                {
                    SocialNetwork = socialNetwork,
                });
            }

            var vm = new SocialProfileViewModel
            {
                Email = "test@test.com",
                UserName = "user",
                SocialLinks = links
            };

            return View(vm);
        }
    }
}
