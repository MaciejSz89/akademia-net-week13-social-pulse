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

        public async Task<IActionResult> Profile()
        {
            var socialNetworks = _mapper.Map<List<SocialNetworkViewModel>>(await _socialNetworkService.GetAsync());
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
        public IActionResult MyLinks()
        {
            var model = new SocialProfileViewModel
            {
                UserName = "John Doe",
                UserLinks = new List<UserLinkViewModel>
                            {
                                new UserLinkViewModel { Id = 1, Title = "Facebook", Url = "https://facebook.com/johndoe" },
                                new UserLinkViewModel { Id = 2, Title = "Twitter", Url = "https://twitter.com/johndoe" }
                            }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddUserLink([FromForm] UserLinkViewModel newLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input data.");
            }

            newLink.Id = new Random().Next(1, 1000); 

            return PartialView("_UserLinkRowPartial", newLink);
        }
    }
}
