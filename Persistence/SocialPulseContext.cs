using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialPulse.Areas.Identity.Data;
using SocialPulse.Core;
using SocialPulse.Core.Models;
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

        builder.Entity<SocialNetwork>().HasData(new SocialNetwork { 
                                                                    Id = 1, 
                                                                    Name = "Facebook", 
                                                                    Url = "https://www.facebook.com", 
                                                                    BaseDomain = "facebook.com", 
                                                                    Icon = new byte[0] 
                                                                  },
                                                new SocialNetwork { 
                                                                    Id = 2, 
                                                                    Name = "Instagram", 
                                                                    Url = "https://www.instagram.com", 
                                                                    BaseDomain = "instagram.com", 
                                                                    Icon = new byte[0] 
                                                                   },
                                                new SocialNetwork { 
                                                                    Id = 3, 
                                                                    Name = "Twitter", 
                                                                    Url = "https://www.twitter.com", 
                                                                    BaseDomain = "twitter.com", 
                                                                    Icon = new byte[0] 
                                                                  });
    }

    public DbSet<UserLink> UserLinks { get; set; }
    public DbSet<SocialLink> SocialLinks { get; set; }
    public DbSet<SocialNetwork> SocialNetworks { get; set; }
    public DbSet<SocialProfile> SocialProfiles { get; set; }
}
