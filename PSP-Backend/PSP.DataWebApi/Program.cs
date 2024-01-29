using Microsoft.AspNetCore.Mvc;
using PSP.DataApplication;

var builder = WebApplication.CreateBuilder(args);

//Configure Endpoints Routing
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

builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddApiVersioning();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));

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
app.UseAuthorization();
app.MapControllers();
app.UseHealthChecks("/health");
app.Run();