using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialPulse.Core.Models;
using SocialPulse.Core.Repositories;
using SocialPulse.Core.Services;
using SocialPulse.Core.ViewModels;
using System.Security.Claims;

namespace SocialPulse.Persistence.Services
{
    public class SocialProfileService : ISocialProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SocialProfileService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public async Task<SocialProfile> GetSocialProfileByUserIdAsync(string userId)
        {
            return await _unitOfWork.SocialProfileRepository.GetByUserIdAsync(userId);
        }

        public async Task UpdateSocialProfileAsync(SocialProfile socialProfile, string? newUserName, string? newEmail)
        {
            if (newUserName != null)
            {
                var setUserNameResult = await _unitOfWork.IdentityUserRepository.SetUserNameAsync(socialProfile.SocialPulseUserId, newUserName);
                if (!setUserNameResult.Succeeded)
                    throw new Exception("SetUserNameAsync failed...");
            }

            if (newEmail != null)
            {
                var setEmailResult = await _unitOfWork.IdentityUserRepository.SetEmailAsync(socialProfile.SocialPulseUserId, newEmail);
                if (!setEmailResult.Succeeded)
                    throw new Exception("SetEmailAsync failed...");
            }

            await _unitOfWork.SocialProfileRepository.UpdateAsync(socialProfile);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
