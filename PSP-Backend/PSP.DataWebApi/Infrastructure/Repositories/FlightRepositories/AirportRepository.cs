using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class AirportRepository(PSPContext context) : IAirportRepository
{
    public async Task<IEnumerable<Airport>> GetAllAsync() => await context.Airports.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<Airport>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.Airports.Skip(index).Take(count).AsNoTracking().ToListAsync();

    public async Task<Airport?> GetByCodeAsync(string code) => await context.Airports.Where(p => p.IataCode == code).AsNoTracking().FirstOrDefaultAsync();

    public async Task<long> GetCountAsync() => await context.Airports.AsNoTracking().CountAsync();

    public async Task<bool> CheckByCodeAsync(string code) => await context.Airports.Where(p => p.IataCode == code).AsNoTracking().AnyAsync();
    
    public async Task<bool> AddAsync(Airport obj)
    {
        await context.Airports.AddAsync(obj);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(Airport obj)
    {
        context.Update(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var dbObj = await context.Airports.Where(p => p.IataCode == code).FirstOrDefaultAsync();
        if (dbObj == null) return false;
        
        context.Airports.Remove(dbObj);
        await context.SaveChangesAsync();
        return true;
    }
}