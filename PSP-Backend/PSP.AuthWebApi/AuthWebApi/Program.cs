using System.Reflection;
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
using Serilog;
using Serilog.Sinks.Elasticsearch;

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

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddIdentityServer()
        .AddAspNetIdentity<PspUser>()
        .AddClientStore<ClientStore>()
        .AddInMemoryIdentityResources(Config.IdentityResources)
        .AddInMemoryApiResources(Config.ApiResources)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddProfileService<UserProfileService>()
        .AddDeveloperSigningCredential();
}
else
{
    builder.Services.AddIdentityServer(x =>
        {
            x.IssuerUri = "https://psp_auth:443";
        })
        .AddAspNetIdentity<PspUser>()
        .AddClientStore<ClientStore>()
        .AddInMemoryIdentityResources(Config.IdentityResources)
        .AddInMemoryApiResources(Config.ApiResources)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddProfileService<UserProfileService>()
        .AddDeveloperSigningCredential();
}

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

ConfigureLogging();
builder.Host.UseSerilog();

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

void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{environment}.json", optional: true
        )
        .Build();
    
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment!))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(Environment.GetEnvironmentVariable("ELASTIC_ROUTE") ?? configuration["ElasticConfiguration:Uri"]!))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
        NumberOfReplicas = 1,
        NumberOfShards = 2
    };
}