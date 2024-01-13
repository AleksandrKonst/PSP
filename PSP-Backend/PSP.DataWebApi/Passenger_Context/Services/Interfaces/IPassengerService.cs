using PSP.DataWebApi.Passenger_Context.DTO;

namespace PSP.DataWebApi.Passenger_Context.Services.Interfaces;

public interface IPassengerService
{
    Task<IEnumerable<PassengerDTO>> GetPassengersAsync(int index, int count);
    Task<int> GetPassengersCountAsync();
    Task<PassengerDTO> GetPassengerByIdAsync(int id);
    Task<bool> AddPassengerAsync(PassengerDTO dto);
    Task<bool> UpdatePassengerAsync(PassengerDTO dto);
    Task<bool> DeletePassengerAsync(int id);
}