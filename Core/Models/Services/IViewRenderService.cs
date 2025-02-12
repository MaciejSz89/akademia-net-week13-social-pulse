namespace SocialPulse.Core.Models.Services;

public interface IViewRenderService
{
    Task<string> RenderToStringAsync(string viewName, object model);
}
