using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialPulse.Core.Dtos;
using SocialPulse.Core.Services;
using SocialPulse.Core.ViewModels;

namespace SocialPulse.Controllers;

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
                            new UserLinkViewModel { 
                                                    Id = 1, 
                                                    Title = "Facebook", 
                                                    Url = "https://facebook.com/johndoe", 
                                                    Image = GetSampleImage("facebook.png") 
                                                  },
                            new UserLinkViewModel { 
                                                    Id = 2, 
                                                    Title = "X", 
                                                    Url = "https://x.com/johndoe" ,
                                                    Image = GetSampleImage("x.png")
                                                  }
                        }
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult RemoveUserLink(int Id)
    {

        if (!ModelState.IsValid)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Invalid input data."
            });
        }

        var response = new ResponseDto
        {
            IsSuccess = true,
        };

        return Json(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddUserLink([FromForm] UserLinkDto newLinkDto, IFormFile image)
    {

        if (!ModelState.IsValid)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Invalid input data."
            });
        }

        var newLink = _mapper.Map<UserLinkViewModel>(newLinkDto);
        newLink.Id = new Random().Next(1, 1000);

        if (image != null)
        {
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                var fileBytes = ms.ToArray();
                newLink.Image = fileBytes;
            }
        }

        var response = new ResponseDto<string>
        {
            IsSuccess = true,
            Data = await _viewRenderService.RenderToStringAsync("_UserLinkRow", newLink)
        };

        return Json(response);
    }

    private byte[]? GetSampleImage(string imageName)
    {
        var filePath = Path.Combine("wwwroot", "images", imageName);
        if (!System.IO.File.Exists(filePath))
        {
            return null;
        }

        var imageBytes = System.IO.File.ReadAllBytes(filePath);
        return imageBytes;
    }
}
