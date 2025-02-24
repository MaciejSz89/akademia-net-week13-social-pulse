namespace SocialPulse.Core.ViewModels
{
    public class InitSocialProfilesViewModel
    {

        public string SessionGuid { get; set; } = null!;
        public IEnumerable<SocialProfileViewModel> SocialProfiles { get; set; } = new List<SocialProfileViewModel>();
        
    }
}
