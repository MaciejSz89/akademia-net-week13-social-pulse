using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialPulse.Areas.Identity.Data;
using SocialPulse.Core;
using SocialPulse.Core.Models;

namespace SocialPulse.Persistence;

public class SocialPulseContext : IdentityDbContext<SocialPulseUser>, ISocialPulseContext
{
    public SocialPulseContext(DbContextOptions<SocialPulseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<UserLinks> UserLinks { get; set; }
    public DbSet<SocialLink> SocialLinks { get; set; }
    public DbSet<SocialNetwork> SocialNetworks { get; set; }
    public DbSet<SocialProfile> SocialProfiles { get; set; }
}
