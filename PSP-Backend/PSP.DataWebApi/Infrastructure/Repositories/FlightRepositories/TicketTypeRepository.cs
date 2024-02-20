using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class TicketTypeRepository(PSPContext context) : ITicketTypeRepository
{
    public async Task<IEnumerable<TicketType>> GetAllAsync() => await context.TicketTypes.ToListAsync();

    public async Task<IEnumerable<TicketType>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.TicketTypes.Skip(index).Take(count).ToListAsync();

    public async Task<TicketType?> GetByCodeAsync(short code) => await context.TicketTypes.Where(p => p.Code == code).FirstOrDefaultAsync();

    public async Task<long> GetCountAsync() => await context.TicketTypes.CountAsync();

    public async Task<bool> CheckByCodeAsync(short code) => await context.TicketTypes.Where(p => p.Code == code).AnyAsync();
    
    public async Task<bool> AddAsync(TicketType obj)
    {
        await context.TicketTypes.AddAsync(obj);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(TicketType obj)
    {
        context.Update(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(short code)
    {
        var dbObj = await context.TicketTypes.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (dbObj == null) return false;
        
        context.TicketTypes.Remove(dbObj);
        await context.SaveChangesAsync();
        return true;
    }
}