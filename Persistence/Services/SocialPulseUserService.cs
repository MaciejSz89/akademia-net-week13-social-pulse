using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SocialPulse.Areas.Identity.Data;
using SocialPulse.Core.Models.Services;
using System.Security.Claims;

namespace SocialPulse.Persistence.Services
{
    public class SocialPulseUserService : ISocialPulseUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<SocialPulseUser> _signInManager;

        private ClaimsPrincipal? CurrentUser => _httpContextAccessor.HttpContext?.User;


        public SocialPulseUserService(IHttpContextAccessor httpContextAccessor, SignInManager<SocialPulseUser> signInManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }


        public string GetCurrentUserEmail()
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value; 
            
            if (email == null)
                throw new NullReferenceException("Email not found");

            return email;
        }

        public string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new NullReferenceException("User not found");

            return userId;
        }

        public string GetCurrentUserName()
        {
            var userName = CurrentUser?.FindFirst(ClaimTypes.Name)?.Value;
            if (userName == null)
                throw new NullReferenceException("User not found");

            return userName;
        }
        public SocialPulseUser GetCurrentUser()
        {
            if (CurrentUser == null)
                throw new NullReferenceException("User not found");

            return new SocialPulseUser
            {
                Id = GetCurrentUserId(),
                UserName = GetCurrentUserName(),
                Email = GetCurrentUserEmail()
            };
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
