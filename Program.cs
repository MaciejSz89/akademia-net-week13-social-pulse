using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialPulse.Areas.Identity.Data;
using SocialPulse.Persistence;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SocialPulseContextConnection") ?? throw new InvalidOperationException("Connection string 'SocialPulseContextConnection' not found.");

builder.Services.AddDbContext<SocialPulseContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SocialPulseUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SocialPulseContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
