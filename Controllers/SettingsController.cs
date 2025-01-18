using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialPulse.Core.Dtos.Response;
using SocialPulse.Core.Services;
using SocialPulse.Core.ViewModels;

namespace SocialPulse.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISocialNetworkService _socialNetworkService;
        private readonly IMapper _mapper;
        private readonly IViewRenderService _viewRenderService;

        public SettingsController(IServiceProvider serviceProvider)
        {
            _socialNetworkService = serviceProvider.GetRequiredService<ISocialNetworkService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _viewRenderService = serviceProvider.GetRequiredService<IViewRenderService>();
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
        public async Task<IActionResult> AddUserLink([FromForm] UserLinkViewModel newLink)
        {
            ResponseDto response;

            if (!ModelState.IsValid)
            {
                response = new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid input data."
                };
            }
            else
            {
                newLink.Id = new Random().Next(1, 1000);

                response = new ResponseDto<string>
                {
                    IsSuccess = true,
                    Data = await _viewRenderService.RenderToStringAsync("_UserLinkRowPartial", newLink)
                };
                
            }          

            return Json(response);
        }
    }
}
