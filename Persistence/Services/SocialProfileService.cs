using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialPulse.Core;
using SocialPulse.Core.Models.Domains;
using SocialPulse.Core.Models.Services;
using SocialPulse.Core.ViewModels;
using SocialPulse.Hubs;
using System.Security.Claims;

namespace SocialPulse.Persistence.Services
{
    public class SocialProfileService : ISocialProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISocialPulseUserService _socialPulseUserService;

        public SocialProfileService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _socialPulseUserService = serviceProvider.GetRequiredService<ISocialPulseUserService>();
        }

        public async Task<SocialProfile> GetSocialProfileAsync()
        {
            var userId = _socialPulseUserService.GetCurrentUserId();

            if (userId == null)
            {
                throw new NullReferenceException("User not found");
            }

            return await _unitOfWork.SocialProfileRepository.GetByUserIdAsync(userId);
        }

        public async Task<(string sessionGuid, IEnumerable<SocialProfile> profiles)> GetInitSocialProfilesAsync(int count)
        {
            var sessionGuid = Guid.NewGuid().ToString();

            var randomProfiles = await GetInitProfiles(sessionGuid, count);
            return (sessionGuid, randomProfiles);
        }

        public async Task<IEnumerable<SocialProfile>> GetNextSocialProfilesAsync(string sessionGuid, int count)
        {
            if (!ProfilesCache.LoadedProfileIdsByGuid.ContainsKey(sessionGuid))
            {
                var initProfiles = await GetInitProfiles(sessionGuid, count);

                return initProfiles;
            }
            else
            {
                var nextProfiles = await GetNextProfiles(sessionGuid, count);

                return nextProfiles;
            }


        }

        private async Task<List<SocialProfile>> GetInitProfiles(string sessionGuid, int count)
        {
            var allProfiles = (await GetSocialProfilesAsync()).ToList();

            var randomProfiles = allProfiles
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .ToList();

            ProfilesCache.LoadedProfileIdsByGuid[sessionGuid] = randomProfiles.Select(p => p.Id)
                                                                               .ToList();
            return randomProfiles;
        }

        private async Task<List<SocialProfile>> GetNextProfiles(string sessionGuid, int count)
        {
            var alreadyReturnedIds = ProfilesCache.LoadedProfileIdsByGuid[sessionGuid];

            var remainingProfiles = (await GetSocialProfilesAsync()).Where(p => !alreadyReturnedIds.Contains(p.Id))
                                                                    .ToList();

            var nextProfiles = remainingProfiles.OrderBy(x => Guid.NewGuid())
                                        .Take(count)
                                        .ToList();

            alreadyReturnedIds.AddRange(nextProfiles.Select(p => p.Id));
            return nextProfiles;
        }

        public async Task<SocialProfile> GetSocialProfileAsync(int id)
        {
            var socialProfile = await _unitOfWork.SocialProfileRepository.GetAsync(id);

            if (socialProfile == null)
                throw new NullReferenceException("Social profile not found");

            return socialProfile;
        }

        public async Task<IEnumerable<SocialProfile>> GetSocialProfilesAsync()
        {
            return await _unitOfWork.SocialProfileRepository.GetAsync();
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
