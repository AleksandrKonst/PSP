using PSP_Data_Service.Passenger_Context.Models;

namespace PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

public interface IPassengerQuotaCountRepository
{
    IQueryable<PassengerQuotaCount> GetAll();
    IQueryable<PassengerQuotaCount> GetByIdAsync(long passengerId, string quotaCategory, string year);
    Task<bool> Add(PassengerQuotaCount passengerQuotaCount);
    Task<bool> Update(PassengerQuotaCount passengerQuotaCount);
    Task<bool> Delete(int id);
}