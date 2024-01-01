using PSP_Data_Service.Passenger_Context.DTO;
using PSP_Data_Service.Passenger_Context.Models;

namespace PSP_Data_Service.Passenger_Context.Services.Interfaces;

public interface IPassengerService
{
    Task<IEnumerable<PassengerDTO>> GetPassengersAsync(int index, int count);
    Task<int> GetPassengersCountAsync();
    Task<Passenger?> GetPassengerByIdAsync(int id);
    Task<bool> AddPassenger(PassengerDTO dto);
    Task<bool> UpdatePassenger(PassengerDTO dto);
    Task<bool> DeletePassenger(int id);
}