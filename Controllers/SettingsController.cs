using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialPulse.Areas.Identity.Data;
using SocialPulse.Core.Dtos;
using SocialPulse.Core.Models;
using SocialPulse.Core.Services;
using SocialPulse.Core.ViewModels;
using System.Security.Claims;

namespace SocialPulse.Controllers;

public class SettingsController : Controller
{
    private readonly ISocialProfileService _socialProfileService;
    private readonly ISocialNetworkService _socialNetworkService;
    private readonly IMapper _mapper;
    private readonly IViewRenderService _viewRenderService;
    private readonly IUserLinkStyleService _userLinkStyleService;
    private readonly SignInManager<SocialPulseUser> _signInManager;
    private readonly UserManager<SocialPulseUser> _userManager;

    public SettingsController(IServiceProvider serviceProvider, 
                              SignInManager<SocialPulseUser> signInManager,
                              UserManager<SocialPulseUser> userManager)
    {
        _socialProfileService = serviceProvider.GetRequiredService<ISocialProfileService>();
        _socialNetworkService = serviceProvider.GetRequiredService<ISocialNetworkService>();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _viewRenderService = serviceProvider.GetRequiredService<IViewRenderService>();
        _userLinkStyleService = serviceProvider.GetRequiredService<IUserLinkStyleService>();
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<IActionResult> Profile()
    {
        var vm = await CreateSocialProfileViewModelAsync();

        if (vm == null)
            return NotFound();

        return View(vm);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveProfile(SocialProfileDto socialProfileDto)
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
            var socialProfile = _mapper.Map<SocialProfile>(socialProfileDto);

            var currentUserName = User.Identity?.Name;
            var currentEmail = User.FindFirstValue(ClaimTypes.Email);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                throw new NullReferenceException("User not found");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new NullReferenceException("User not found");

            socialProfile.SocialPulseUserId = userId;

            var newUserName = socialProfileDto.UserName != currentUserName ? socialProfileDto.UserName : null;
            var newEmail = socialProfileDto.Email != currentEmail ? socialProfileDto.Email : null;

            await _socialProfileService.UpdateSocialProfileAsync(socialProfile,
                                                                 newUserName,
                                                                 newEmail);

            if (newUserName != null || newEmail != null)
                await _signInManager.RefreshSignInAsync(user);
        }
        catch (Exception)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Coś poszło nie tak"
            });
        }

        return Json(new ResponseDto<string?>
        {
            IsSuccess = true,
            Data = User.Identity?.Name
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

    private async Task<SocialProfileViewModel?> CreateSocialProfileViewModelAsync()
    {
        var socialNetworks = _mapper.Map<List<SocialNetworkViewModel>>(await _socialNetworkService.GetAsync());

        var userName = User.Identity?.Name;
        var email = User.FindFirstValue(ClaimTypes.Email);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


        if (email == null || userName == null || userId == null)
            return null;

        var socialProfile = await _socialProfileService.GetSocialProfileByUserIdAsync(userId);

        List<SocialLinkViewModel> links = new List<SocialLinkViewModel>();

        foreach (var socialNetwork in socialNetworks)
        {
            var socialLink = new SocialLinkViewModel
            {
                SocialNetworkId = socialNetwork.Id,
                SocialNetwork = socialNetwork,
                RemainingUrl = socialProfile.SocialLinks
                                            .SingleOrDefault(x => x.SocialNetworkId == socialNetwork.Id)?
                                            .RemainingUrl
            };

            links.Add(socialLink);
        }

        var vm = new SocialProfileViewModel
        {
            Id = socialProfile.Id,
            Email = email,
            UserName = userName,
            SocialLinks = links,
            ProfileImage = socialProfile.ProfileImage,
            Content = socialProfile.Content            
        };

        return vm;

    }

}
