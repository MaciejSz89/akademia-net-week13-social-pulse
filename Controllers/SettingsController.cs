using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SocialPulse.Areas.Identity.Data;
using SocialPulse.Core.Dtos;
using SocialPulse.Core.Models.Domains;
using SocialPulse.Core.Models.Services;
using SocialPulse.Core.ViewModels;
using System.Security.Claims;

namespace SocialPulse.Controllers;

[Authorize]
public class SettingsController : Controller
{
    private readonly ISocialProfileService _socialProfileService;
    private readonly ISocialNetworkService _socialNetworkService;
    private readonly IUserLinkService _userLinkService;
    private readonly ISocialPulseUserService _socialPulseUserService;
    private readonly IMapper _mapper;
    private readonly IViewRenderService _viewRenderService;
    private readonly IUserLinkStyleService _userLinkStyleService;
    private readonly SignInManager<SocialPulseUser> _signInManager;

    public SettingsController(IServiceProvider serviceProvider,
                              SignInManager<SocialPulseUser> signInManager,
                              UserManager<SocialPulseUser> userManager)
    {
        _socialProfileService = serviceProvider.GetRequiredService<ISocialProfileService>();
        _socialNetworkService = serviceProvider.GetRequiredService<ISocialNetworkService>();
        _userLinkService = serviceProvider.GetRequiredService<IUserLinkService>();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _viewRenderService = serviceProvider.GetRequiredService<IViewRenderService>();
        _userLinkStyleService = serviceProvider.GetRequiredService<IUserLinkStyleService>();
        _socialPulseUserService = serviceProvider.GetRequiredService<ISocialPulseUserService>();
        _signInManager = signInManager;
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

            var currentUserName = _socialPulseUserService.GetCurrentUserName();
            var currentEmail = _socialPulseUserService.GetCurrentUserEmail();
            var userId = _socialPulseUserService.GetCurrentUserId();

            if (userId == null)
                throw new NullReferenceException("User not found");

            var user = _socialPulseUserService.GetCurrentUser();

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
            Data = _socialPulseUserService.GetCurrentUserName()
        });
    }


    public async Task<IActionResult> UserLinks()
    {
        try
        {
            var userId = _socialPulseUserService.GetCurrentUserId();

            var userLinks = await _userLinkService.GetUserLinksAsync(userId);

            var vm = new SocialProfileViewModel
            {
                UserName = _socialPulseUserService.GetCurrentUserName(),
                UserLinks = _mapper.Map<IEnumerable<UserLinkViewModel>>(userLinks).ToList()
            };
            return View(vm);
        }
        catch (Exception)
        {
            return NotFound();
        }

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddUserLink([FromForm] UserLinkDto userLink)
    {

        if (!ModelState.IsValid)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Coś poszło nie tak."
            });
        }

        try
        {
            var newLink = _mapper.Map<UserLink>(userLink);

            newLink.SocialProfileId = (await _socialProfileService.GetSocialProfileAsync()).Id;

            await _userLinkService.AddUserLinkAsync(newLink);

            return Json(new ResponseDto<string>
            {
                IsSuccess = true,
                Data = await _viewRenderService.RenderToStringAsync("_UserLinkRow", _mapper.Map<UserLinkViewModel>(newLink))
            });

        }
        catch (Exception)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Coś poszło nie tak."
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveUserLink(int id)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Coś poszło nie tak."
            });
        }

        try
        {
            var socialProfileId = (await _socialProfileService.GetSocialProfileAsync()).Id;

            await _userLinkService.RemoveUserLinkAsync(id);

            var response = new ResponseDto
            {
                IsSuccess = true,
            };

            return Json(response);
        }
        catch (Exception)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Coś poszło nie tak."
            });
        }


    }

    [HttpPost]
    public async Task<IActionResult> SaveUserLink([FromForm] UserLinkDto userLink)
    {

        if (!ModelState.IsValid)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Coś poszło nie tak."
            });
        }

        try
        {
            var socialProfileId = (await _socialProfileService.GetSocialProfileAsync()).Id;

            var userLinkToUpdate = _mapper.Map<UserLink>(userLink);

            userLinkToUpdate.SocialProfileId = socialProfileId;

            await _userLinkService.UpdateUserLinkAsync(userLinkToUpdate);

            var response = new ResponseDto
            {
                IsSuccess = true
            };

            return Json(response);
        }
        catch (Exception)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Coś poszło nie tak."
            });
        }

    }

    public async Task<IActionResult> LinksStyles()
    {
        try
        {
            var vm = await CreateUserLinkStyleViewModelAsync();
            return View(vm);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveProfile()
    {
        try
        {
            await _socialProfileService.RemoveSocialProfileAsync();

            return Json(new ResponseDto
            {
                IsSuccess = true
            });
        }
        catch
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Coś poszło nie tak."
            });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveLinkStyle(string userLinkStyle)
    {
        try
        {
            await _userLinkStyleService.UpdateUserLinkStyleAsync(userLinkStyle);

            return Json(new ResponseDto<string>
            {
                IsSuccess = true,
                Data = userLinkStyle
            });
        }
        catch (Exception)
        {
            return Json(new ResponseDto
            {
                IsSuccess = false,
                Message = "Coś poszło nie tak."
            });
        }


    }


    private async Task<SocialProfileViewModel> CreateSocialProfileViewModelAsync()
    {
        var socialNetworks = _mapper.Map<List<SocialNetworkViewModel>>(await _socialNetworkService.GetAsync());

        var socialProfile = await _socialProfileService.GetSocialProfileAsync();

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
            Email = _socialPulseUserService.GetCurrentUserEmail(),
            UserName = _socialPulseUserService.GetCurrentUserName(),
            SocialLinks = links,
            ProfileImage = socialProfile.ProfileImage,
            Content = socialProfile.Content,

        };

        return vm;

    }

    private async Task<UserLinkStyleViewModel> CreateUserLinkStyleViewModelAsync()
    {
        return new UserLinkStyleViewModel
        {
            UserName = _socialPulseUserService.GetCurrentUserName(),
            CurrentUserLinkStyle = await _userLinkStyleService.GetCurrentUserLinkStyleAsync(),
            UserLinkStyles = _userLinkStyleService.GetUserLinkStyles()
        };
    }

}
