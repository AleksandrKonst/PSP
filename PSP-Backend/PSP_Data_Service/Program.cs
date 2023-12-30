using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;
using PSP_Data_Service.Passenger_Context.Services;
using PSP_Data_Service.Passenger_Context.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddApiVersioning();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<PassengerDataContext>();

builder.Services.AddTransient<IGenderTypeRepository, GenderTypeRepository>();

builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddTransient<IPassengerRepository, PassengerRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health");

app.Run();