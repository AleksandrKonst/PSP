using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class CouponEventRepository(PSPContext context) : ICouponEventRepository
{
    public async Task<List<CouponEvent>> GetAllAsync(int ticketType, string ticketNumber) => await context.CouponEvents.Where(c => c.TicketType == ticketType && c.TicketNumber == ticketNumber).ToListAsync();

    public async Task<bool> CheckByIdAsync(long id) => await context.CouponEvents.Where(c => c.Id == id).AnyAsync();

    public async Task<bool> AddAsync(CouponEvent couponEvent)
    {
        var newCouponEvent = await context.CouponEvents.AddAsync(couponEvent);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<int> DeleteByTicketAsync(string operationType, int ticketType, string ticketNumber, DateTime operationDataTime)
    {
        var dbCoupons = context.CouponEvents
            .Where(p => p.OperationType == operationType && 
                        p.TicketType == ticketType && p.TicketNumber == ticketNumber && 
                        p.OperationDatetimeUtc == operationDataTime);
        
        context.CouponEvents.RemoveRange(dbCoupons);
        var count = await context.SaveChangesAsync();
        return count;
    }
}