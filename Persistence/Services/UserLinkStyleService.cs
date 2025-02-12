using SocialPulse.Core.Models.Services;

namespace SocialPulse.Persistence.Services
{
    public class UserLinkStyleService : IUserLinkStyleService
    {
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
    }
}
