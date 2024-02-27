using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("IdentityApiKey")
    .AddJwtBearer("IdentityApiKey", options =>
    {
        options.Authority = "https://localhost:7161";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddOcelot();
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyOrigin();
    policyBuilder.AllowAnyHeader();
    policyBuilder.AllowAnyMethod();
});
app.UseOcelot().Wait();
app.MapControllers();
app.Run();