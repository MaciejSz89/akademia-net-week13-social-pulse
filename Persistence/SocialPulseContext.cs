using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialPulse.Areas.Identity.Data;
using SocialPulse.Core;
using SocialPulse.Core.Models.Domains;
using System.Reflection.Emit;

namespace SocialPulse.Persistence;

public class SocialPulseContext : IdentityDbContext<SocialPulseUser>
{
    public SocialPulseContext(DbContextOptions<SocialPulseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SocialNetwork>()
               .Property(s => s.Id)
               .ValueGeneratedNever();
    }

    public DbSet<UserLink> UserLinks { get; set; }
    public DbSet<SocialLink> SocialLinks { get; set; }
    public DbSet<SocialNetwork> SocialNetworks { get; set; }
    public DbSet<SocialProfile> SocialProfiles { get; set; }
}
