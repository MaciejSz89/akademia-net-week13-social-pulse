using Microsoft.AspNetCore.Identity;

namespace SocialPulse.Core.Models.Repositories
{
    public interface IIdentityUserRepository
    {
        Task<IdentityResult> SetUserNameAsync(string userId, string userName);
        Task<IdentityResult> SetEmailAsync(string userId, string email);
    }
}
