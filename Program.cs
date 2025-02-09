using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocialPulse.Areas.Identity.Data;
using SocialPulse.Core;
using SocialPulse.Core.Models.Settings;
using SocialPulse.Core.Repositories;
using SocialPulse.Core.Services;
using SocialPulse.Persistence;
using SocialPulse.Persistence.Repositories;
using SocialPulse.Persistence.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SocialPulseContextConnection") ?? throw new InvalidOperationException("Connection string 'SocialPulseContextConnection' not found.");

builder.Services.AddDbContext<SocialPulseContext>(options => options.UseSqlServer(connectionString));
var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
bool isEmailConfigured = !string.IsNullOrWhiteSpace(emailSettings?.SmtpServer) &&
                         emailSettings.SmtpPort > 0 &&
                         !string.IsNullOrWhiteSpace(emailSettings?.FromEmail) &&
                         !string.IsNullOrWhiteSpace(emailSettings?.FromPassword);

builder.Services.AddDefaultIdentity<SocialPulseUser>(options =>
                                                    {
                                                        options.SignIn.RequireConfirmedAccount = isEmailConfigured; // Require confirmation only if email is configured
                                                        options.User.RequireUniqueEmail = true;        // Ensure unique email addresses
                                                    })
                .AddEntityFrameworkStores<SocialPulseContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ISocialNetworkRepository, SocialNetworkRepository>()
                .AddScoped<ISocialProfileRepository, SocialProfileRepository>()
                .AddScoped<ISocialNetworkService, SocialNetworkService>()
                .AddScoped<IUserLinkStyleService, UserLinkStyleService>()
                .AddScoped<ISocialProfileService, SocialProfileService>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IViewRenderService, ViewRenderService>()
                .AddHttpContextAccessor(); 

// Register the email sender if email settings are configured
if (isEmailConfigured)
{
    builder.Services.AddTransient<IEmailSender>(provider =>
        new SmtpEmailSender(
            emailSettings!.SmtpServer,
            emailSettings.SmtpPort,
            emailSettings.FromEmail,
            emailSettings.FromPassword
        ));
}
else
{
    // No-op email sender if email settings are missing
    builder.Services.AddTransient<IEmailSender, NoOpEmailSender>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SocialPulseContext>();
    dbContext.Database.EnsureCreated();

    var socialNetworkService = scope.ServiceProvider.GetRequiredService<ISocialNetworkService>();
    var socialNetworksJsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/social_networks.json");

    await socialNetworkService.PopulateSocialNetworksFromJsonAsync(socialNetworksJsonFilePath);
}


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
