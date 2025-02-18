namespace SocialPulse.Core.Models.Services
{
    public interface IUserLinkStyleService
    {
        Task<string> GetCurrentUserLinkStyleAsync();
        List<string> GetUserLinkStyles();
        Task UpdateUserLinkStyleAsync(string userLinkStyle);
    }
}
