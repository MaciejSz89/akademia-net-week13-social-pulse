using SocialPulse.Core;
using SocialPulse.Core.Models.Domains;
using SocialPulse.Core.Models.Services;

namespace SocialPulse.Persistence.Services
{
    public class UserLinkService : IUserLinkService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserLinkService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }
        public async Task<IEnumerable<UserLink>> GetUserLinksAsync(string userId)
        {
            return await _unitOfWork.UserLinkRepository.GetUserLinksByUserIdAsync(userId);
        }
    }
}
