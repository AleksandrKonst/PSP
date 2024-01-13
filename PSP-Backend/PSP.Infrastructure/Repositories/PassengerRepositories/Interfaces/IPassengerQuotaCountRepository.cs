using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IPassengerQuotaCountRepository
{
    IQueryable<PassengerQuotaCount> GetAll();
    Task<PassengerQuotaCount> GetByIdAsync(long passengerId, string quotaCategory, string year);
    Task<bool> Add(PassengerQuotaCount passengerQuotaCount);
    Task<bool> Update(PassengerQuotaCount passengerQuotaCount);
    Task<bool> Delete(long id);
}