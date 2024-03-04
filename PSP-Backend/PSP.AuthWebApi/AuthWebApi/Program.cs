using AuthWebApi;
using AuthWebApi.Middleware;
using AuthWebApi.Models;
using AuthWebApi.Models.Data;
using AuthWebApi.Models.Infrastructure;
using AuthWebApi.Reporters;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MetricReporter>();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("DB_ROUTE") ?? builder.Configuration.GetConnectionString("PSPAuthContext")));

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
    config.Cookie.Name = Environment.GetEnvironmentVariable("COOKIE_NANE") ?? "PSP.IdentityServer.Cookie";
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
        options.ClientId = Environment.GetEnvironmentVariable("YANDEX_CLIENTID") ?? "adf944112ee74aaca09676257c64016a";
        options.ClientSecret = Environment.GetEnvironmentVariable("YANDEX_SECRET") ?? "c73aaa4b285a43739a940ba8d83a9533";
        options.CallbackPath = Environment.GetEnvironmentVariable("YANDEX_CALLBACK_PATH") ?? "/Auth/yandex";
    })
    .AddVkontakte(options =>
    {
        options.Scope.Add("email");
        options.ClientId = Environment.GetEnvironmentVariable("VK_CLIENTID") ?? "51863166";
        options.ClientSecret = Environment.GetEnvironmentVariable("VK_SECRET") ?? "vEo11t3WIkNLe2zcAm1z";
        options.CallbackPath = Environment.GetEnvironmentVariable("VK_CALLBACK_PATH") ?? "/Auth/vk";
    });

builder.Services.AddCors(options => options.AddPolicy(name: "PSP",
    policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHealthChecks();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("PSP");
app.UseAuthentication();
app.UseAuthorization();
app.UseMetricServer();
app.UseMiddleware<ResponseMetricMiddleware>();
app.UseIdentityServer();
app.MapDefaultControllerRoute();
app.UseHealthChecks("/health");
app.Run();