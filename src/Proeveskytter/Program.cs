using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Proeveskytter.Data;
using Proeveskytter.Models;
using Proeveskytter.Services;
using System.Configuration;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseContextConnection' not found."); ;

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<DatabaseContext>();

// Add services to the container.
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// MVC til resten af appen
builder.Services.AddControllersWithViews();

// Razor Pages til Identity UI
builder.Services.AddRazorPages();


builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = false;
    options.MaxAge = TimeSpan.FromDays(730);
});

builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// MVC routes til din app
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Razor Pages endpoints til Identity
app.MapRazorPages();

app.Run();
