using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.FlightRepositories;

public class CouponEventRepository(PSPContext context) : ICouponEventRepository
{
    public async Task<bool> CheckByIdAsync(long id) => await context.DataCouponEvents.Where(c => c.Id == id).AnyAsync();

    public async Task<bool> AddAsync(CouponEvent couponEvent)
    {
        var newCouponEvent = await context.DataCouponEvents.AddAsync(couponEvent);
        await context.SaveChangesAsync();
        return true;
    }
}