using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface ICouponEventRepository
{
    Task<List<CouponEvent>> GetAllAsync(int ticketType, string ticketNumber);
    Task<bool> CheckByIdAsync(long id);
    Task<bool> AddAsync(CouponEvent couponEvent);
    Task<int> DeleteByTicketAsync(string operationType, int ticketType, string ticketNumber, DateTime operationDataTime);
}