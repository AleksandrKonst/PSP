using Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Prometheus;
using WebApi.Middleware;
using WebApi.Reporters;

var builder = WebApplication.CreateBuilder(args);

//Configure Endpoints Routing
builder.Services.AddSingleton<MetricReporter>();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(apiBehaviorOptions =>
    apiBehaviorOptions.InvalidModelStateResponseFactory = actionContext => {
        return new BadRequestObjectResult(new {
            trace_id = Guid.NewGuid().ToString(),
            errors = actionContext.ModelState.Values.SelectMany(x => x.Errors)
                .Select(x => new
                {
                    code = "PPC-000403",
                    message = x.ErrorMessage
                })
        });
    });
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7161"; //Use Environment param
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddApiVersioning();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();

//Add Any DI Configuration Block
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

//Environment.IsDevelopment Middleware Block
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Middleware Block
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMetricServer();
app.UseMiddleware<ResponseMetricMiddleware>();
app.MapControllers();
app.UseHealthChecks("/health");
app.Run();