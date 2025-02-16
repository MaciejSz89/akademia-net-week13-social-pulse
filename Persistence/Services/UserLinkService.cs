using SocialPulse.Core;
using SocialPulse.Core.Models.Domains;
using SocialPulse.Core.Models.Services;

namespace SocialPulse.Persistence.Services
{
    public class UserLinkService : IUserLinkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISocialProfileService _socialProfileService;

        public UserLinkService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _socialProfileService = serviceProvider.GetRequiredService<ISocialProfileService>();
        }

        public async Task<int> AddUserLinkAsync(UserLink userLink)
        {
            await _unitOfWork.UserLinkRepository.AddUserLinkAsync(userLink);

            await _unitOfWork.SaveChangesAsync();

            return userLink.Id;
        }

        public async Task<IEnumerable<UserLink>> GetUserLinksAsync(string userId)
        {
            return await _unitOfWork.UserLinkRepository.GetUserLinksByUserIdAsync(userId);
        }

        public async Task RemoveUserLinkAsync(int userLinkId)
        {
            var socialProfileId = (await _socialProfileService.GetSocialProfileAsync()).Id;

            await _unitOfWork.UserLinkRepository.RemoveUserLinkAsync(userLinkId, socialProfileId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserLinkAsync(UserLink userLink)
        {
            await _unitOfWork.UserLinkRepository.UpdateUserLinkAsync(userLink);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
