using SocialPulse.Core;
using SocialPulse.Core.Models.Services;

namespace SocialPulse.Persistence.Services
{
    public class UserLinkStyleService : IUserLinkStyleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISocialPulseUserService _socialPulseUserService;

        public UserLinkStyleService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _socialPulseUserService = serviceProvider.GetRequiredService<ISocialPulseUserService>();
        }
        public async Task<string> GetCurrentUserLinkStyleAsync()
        {
            var userId = _socialPulseUserService.GetCurrentUserId();
            var socialProfile = await _unitOfWork.SocialProfileRepository.GetByUserIdAsync(userId);

            return socialProfile.UserLinkStyle;
        }

        public List<string> GetUserLinkStyles()
        {
            return new List<string>
            {
                "btn-primary",
                "btn-secondary",
                "btn-success",
                "btn-danger",
                "btn-warning",
                "btn-info",
                "btn-light",
                "btn-dark"
            };
        }

        public async Task UpdateUserLinkStyleAsync(string userLinkStyle)
        {
            var userId = _socialPulseUserService.GetCurrentUserId();
            var socialProfileToUpdate = await _unitOfWork.SocialProfileRepository.GetByUserIdAsync(userId);

            socialProfileToUpdate.UserLinkStyle = userLinkStyle;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
