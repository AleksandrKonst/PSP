using AuthWebApi;
using AuthWebApi.Models;
using AuthWebApi.Models.Data;
using AuthWebApi.Models.Infrastructure;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PSPAuthContext")));

builder.Services.AddIdentity<PspUser, IdentityRole>(config =>
    {
        config.Password.RequiredLength = 4;
        config.Password.RequireDigit = true;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = true;
        config.Password.RequireLowercase = true;
        config.User.RequireUniqueEmail = true;
        config.SignIn.RequireConfirmedEmail = true;
    })
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "PSP.IdentityServer.Cookie";
    config.Cookie.SameSite = SameSiteMode.None;
    config.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IClientStore, ClientStore>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<PspUser>()
    .AddClientStore<ClientStore>()
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddProfileService<UserProfileService>()
    .AddDeveloperSigningCredential();

builder.Services.AddAuthentication()
    .AddYandex(options =>
    {
        options.ClientId = "44e78c9feddc4e23980586255a8eaaa7";
        options.ClientSecret = "308c2f61275c415481a540180a86b247";
        options.CallbackPath = "/Auth/ExternalLoginCallback";
    });

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();
app.MapDefaultControllerRoute();
app.Run();