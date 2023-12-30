namespace PSP_Data_Service.Passenger_Context.Services.Interfaces;

public interface IPassengerService
{
    Task<dynamic> GetPassengersAsync(DateTime requestDateTime);
}