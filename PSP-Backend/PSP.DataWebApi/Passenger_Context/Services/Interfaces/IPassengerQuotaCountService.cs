using PSP.DataWebApi.Passenger_Context.DTO;

namespace PSP.DataWebApi.Passenger_Context.Services.Interfaces;

public interface IPassengerQuotaCountService
{
    Task<IEnumerable<PassengerQuotaCountDTO>> GetPassengerQuotaCountsAsync(int index, int count);
    Task<int> GetPassengerQuotaCountLenghtAsync();
    Task<PassengerQuotaCountDTO> GetPassengerQuotaCountByIdAsync(long passengerId, string quotaCategory, string year);
    Task<bool> AddPassengerQuotaCountAsync(PassengerQuotaCountDTO dto);
    Task<bool> UpdatePassengerQuotaCountAsync(PassengerQuotaCountDTO dto);
    Task<bool> DeletePassengerQuotaCountAsync(long id);
}