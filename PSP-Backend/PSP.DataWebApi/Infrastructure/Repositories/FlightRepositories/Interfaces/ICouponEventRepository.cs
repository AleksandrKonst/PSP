using Domain.Models;

namespace Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface ICouponEventRepository : ICrudRepository<CouponEvent, long>
{
    Task<List<CouponEvent>> GetAllAsync(int ticketType, string ticketNumber);
    Task<bool> CheckByIdAsync(long id);
    Task<int> DeleteByTicketAsync(string operationType, int ticketType, string ticketNumber, DateTime operationDataTime);
}