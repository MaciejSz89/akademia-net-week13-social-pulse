using Microsoft.EntityFrameworkCore;
using SocialPulse.Core.Models;

namespace SocialPulse.Core
{
    public interface ISocialPulseContext
    {
        DbSet<UserLinks> UserLinks { get; set; }
        DbSet<SocialLink> SocialLinks { get; set; }
        DbSet<SocialNetwork> SocialNetworks { get; set; }
        DbSet<SocialProfile> SocialProfiles { get; set; }
    }
}