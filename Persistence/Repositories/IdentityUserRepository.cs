using Microsoft.AspNetCore.Identity;
using SocialPulse.Areas.Identity.Data;
using SocialPulse.Core.Repositories;

namespace SocialPulse.Persistence.Repositories
{
    public class IdentityUserRepository : IIdentityUserRepository
    {
        private readonly UserManager<SocialPulseUser> _userManager;

        public IdentityUserRepository(UserManager<SocialPulseUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> SetUserNameAsync(string userId, string newUserName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Code = "NotFound", Description = "User not found." });

            return await _userManager.SetUserNameAsync(user, newUserName);
        }

        public async Task<IdentityResult> SetEmailAsync(string userId, string newEmail)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Code = "NotFound", Description = "User not found." });

            return await _userManager.SetEmailAsync(user, newEmail);
        }
    }
}
