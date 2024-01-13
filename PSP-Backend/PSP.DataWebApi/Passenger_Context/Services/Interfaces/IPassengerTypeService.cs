using PSP.DataWebApi.Passenger_Context.DTO;

namespace PSP.DataWebApi.Passenger_Context.Services.Interfaces;

public interface IPassengerTypeService
{
    Task<IEnumerable<PassengerTypeDTO>> GetPassengerTypesAsync(int index, int count);
    Task<int> GetPassengerTypesCountAsync();
    Task<PassengerTypeDTO> GetPassengerTypeByIdAsync(string id);
    Task<bool> AddPassengerTypeAsync(PassengerTypeDTO dto);
    Task<bool> UpdatePassengerTypeAsync(PassengerTypeDTO dto);
    Task<bool> DeletePassengerTypeAsync(string id);
}