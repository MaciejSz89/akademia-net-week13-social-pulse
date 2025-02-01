using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialPulse.Core.Dtos;
using SocialPulse.Core.Models;
using SocialPulse.Core.Services;
using SocialPulse.Core.ViewModels;
using System.Security.Claims;

namespace SocialPulse.Controllers;

public class SettingsController : Controller
{
    private readonly ISocialProfileService _socialProfileService;
    private readonly IMapper _mapper;
    private readonly IViewRenderService _viewRenderService;
    private readonly IUserLinkStyleService _userLinkStyleService;

    public SettingsController(IServiceProvider serviceProvider)
    {
        _socialProfileService = serviceProvider.GetRequiredService<ISocialProfileService>();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _viewRenderService = serviceProvider.GetRequiredService<IViewRenderService>();
        _userLinkStyleService = serviceProvider.GetRequiredService<IUserLinkStyleService>();
    }

    public async Task<IActionResult> Profile()
    {
        var vm = await _socialProfileService.CreateSocialProfileViewModelAsync(User);

        if (vm == null) 
            return NotFound();

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveProfile(SocialProfileDto socialProfile)
    {

        if (!ModelState.IsValid)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Coś poszło nie tak"
            });
        }

        try
        {

        }
        catch (Exception)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Coś poszło nie tak"
            });
        }

        return Json(new ResponseDto
        {
            IsSuccess = true
        });
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

    public IActionResult LinksStyles()
    {
        var vm = new UserLinkStyleViewModel
        {
            SocialProfile = new SocialProfile
            {
                UserLinkStyle = "btn-secondary",
                SocialPulseUser = new Areas.Identity.Data.SocialPulseUser
                {
                    UserName = "User1"
                }
            },
            UserLinkStyles = _userLinkStyleService.GetUserLinkStyles()
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveStyle(string userLinkStyle)
    {
        return Json(new ResponseDto<string>
        {
            IsSuccess = true,
            Data = userLinkStyle
        });
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
    public async Task<IActionResult> AddUserLink([FromForm] UserLinkDto newLinkDto, IFormFile? image)
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

    [HttpPost]
    public IActionResult SaveUserLink([FromForm] UserLinkDto updatedLinkDto, IFormFile? image)
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
            IsSuccess = true
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
