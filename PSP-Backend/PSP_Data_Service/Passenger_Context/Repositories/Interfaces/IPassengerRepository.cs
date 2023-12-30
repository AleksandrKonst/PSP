using PSP_Data_Service.Passenger_Context.Models;

namespace PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

public interface IPassengerRepository
{
    Task<IEnumerable<Passenger>> GetAllAsync();
}