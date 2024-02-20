using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class CouponEventRepository(PSPContext context) : ICouponEventRepository
{
    public async Task<IEnumerable<CouponEvent>> GetAllAsync() => await context.CouponEvents.ToListAsync();

    public async Task<IEnumerable<CouponEvent>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.CouponEvents.Skip(index).Take(count).ToListAsync();

    public async Task<CouponEvent?> GetByCodeAsync(long id) => await context.CouponEvents.Where(p => p.Id == id).FirstOrDefaultAsync();

    public async Task<long> GetCountAsync() => await context.CouponEvents.CountAsync();

    public async Task<List<CouponEvent>> GetAllAsync(int ticketType, string ticketNumber) => await context.CouponEvents.Where(c => c.TicketType == ticketType && c.TicketNumber == ticketNumber).ToListAsync();

    public async Task<bool> CheckByCodeAsync(long code) => await context.CouponEvents.Where(p => p.Id == code).AnyAsync();
    
    public async Task<bool> CheckByIdAsync(long id) => await context.CouponEvents.Where(c => c.Id == id).AnyAsync();

    public async Task<bool> AddAsync(CouponEvent couponEvent)
    {
        var newCouponEvent = await context.CouponEvents.AddAsync(couponEvent);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(CouponEvent couponEvent)
    {
        context.Update(couponEvent);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var dbObj = await context.CouponEvents.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (dbObj == null) return false;
        
        context.CouponEvents.Remove(dbObj);
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