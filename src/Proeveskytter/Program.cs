using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Proeveskytter.Data;
using Proeveskytter.Models;
using Proeveskytter.Models.Security;
using Proeveskytter.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseContextConnection' not found."); ;



builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<DatabaseContext>();

// Add services to the container.
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<SecurityOptions>(builder.Configuration.GetSection("Security"));

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

builder.Services.AddAntiforgery( options =>
        {
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

var app = builder.Build();

// 👇 Apply migrations automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();
}


app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();


app.UseHttpsRedirection();
app.UseRouting();

app.Use(async (context, next) =>
{
    var options = context.RequestServices
        .GetRequiredService<IOptions<SecurityOptions>>()
        .Value;

    foreach (var header in options.Headers)
    {
        if (!string.IsNullOrWhiteSpace(header.Name) &&
            !string.IsNullOrWhiteSpace(header.Value))
        {
            context.Response.Headers[header.Name] = header.Value;
        }
    }

    await next();
});
app.UseAuthorization();

app.UseStaticFiles();

// MVC routes til din app
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Razor Pages endpoints til Identity
app.MapRazorPages();

app.Run();
