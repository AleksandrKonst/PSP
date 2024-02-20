using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class FlightRepository(PSPContext context) : IFlightRepository
{
    public async Task<IEnumerable<Flight>> GetAllAsync() => await context.Flights.ToListAsync();

    public async Task<IEnumerable<Flight>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.Flights.Skip(index).Take(count).ToListAsync();

    public async Task<long> GetCountAsync() => await context.Flights.CountAsync();
    
    
    public async Task<bool> AddAsync(Flight obj)
    {
        await context.Flights.AddAsync(obj);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(Flight obj)
    {
        context.Update(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long code)
    {
        var dbObj = await context.Flights.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (dbObj == null) return false;
        
        context.Flights.Remove(dbObj);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<Flight?> GetByCodeAsync(long code) => await context.Flights.Where(p => p.Code == code).FirstOrDefaultAsync();
    
    public async Task<bool> CheckByCodeAsync(long code) => await context.Flights.Where(f => f.Code == code).AnyAsync();
}