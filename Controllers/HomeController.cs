using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialPulse.Core.Dtos;
using SocialPulse.Core.Models.Services;
using SocialPulse.Core.ViewModels;
using System.Diagnostics;

namespace SocialPulse.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly ISocialProfileService _socialProfileService;
    private const int initSocialProfileCount = 9;
    private const int nextSocialProfileCount = 6;

    public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _socialProfileService = serviceProvider.GetRequiredService<ISocialProfileService>();
    }

    public async Task<IActionResult> Index()
    {
        var vm = await CreateInitSocialProfilesViewModel();

        return View(vm);
    }

    public async Task<IActionResult> GetNextProfiles(string sessionGuid)
    {
        try
        {
            var profiles = _mapper.Map<IEnumerable<SocialProfileViewModel>>(await _socialProfileService.GetNextSocialProfilesAsync(sessionGuid, nextSocialProfileCount));

            if (!profiles.Any())
            {
                return new EmptyResult();
            }

            return PartialView("_NextProfiles", profiles);
        }
        catch (Exception)
        {
            return new EmptyResult();
        }

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> ViewProfile(int id)
    {
        var vm = await CreateSocialProfileViewModel(id);

        return View(vm);
    }

    private async Task<SocialProfileViewModel> CreateSocialProfileViewModel(int id)
    {
        var vm = _mapper.Map<SocialProfileViewModel>(await _socialProfileService.GetSocialProfileAsync(id));

        return vm;
    }

    private async Task<InitSocialProfilesViewModel> CreateInitSocialProfilesViewModel()
    {
        var initSet = await _socialProfileService.GetInitSocialProfilesAsync(initSocialProfileCount);

        var sessionGuid = initSet.sessionGuid;
        var socialProfiles = initSet.profiles;

        return new InitSocialProfilesViewModel
        {
            SocialProfiles = _mapper.Map<IEnumerable<SocialProfileViewModel>>(socialProfiles),
            SessionGuid = sessionGuid
        };
    }

}
