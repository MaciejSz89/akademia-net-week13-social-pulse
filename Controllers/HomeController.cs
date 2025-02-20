using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialPulse.Core.Models.Services;
using SocialPulse.Core.ViewModels;
using System.Diagnostics;

namespace SocialPulse.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly ISocialProfileService _socialProfileService;

    public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _socialProfileService = serviceProvider.GetRequiredService<ISocialProfileService>();
    }

    public IActionResult Index()
    {
        return View();
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
}
