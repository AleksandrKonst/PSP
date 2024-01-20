using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IPassengerQuotaCountRepository
{
    Task<List<PassengerQuotaCount>> GetAllAsync();
    Task<List<PassengerQuotaCount>> GetPartAsync(int index = 0, int count = Int32.MaxValue);
    Task<PassengerQuotaCount> GetByIdAsync(long passengerId, string quotaCategory, int year);
    Task<int> GetCountAsync();
    Task<bool> AddAsync(PassengerQuotaCount passengerQuotaCount);
    Task<bool> UpdateAsync(PassengerQuotaCount passengerQuotaCount);
    Task<bool> DeleteAsync(long id);
}