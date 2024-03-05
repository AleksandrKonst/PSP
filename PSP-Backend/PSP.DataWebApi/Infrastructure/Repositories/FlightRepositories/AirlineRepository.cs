using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class AirlineRepository(PSPContext context) : IAirlineRepository
{
    public async Task<IEnumerable<Airline>> GetAllAsync() => await context.Airlines.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<Airline>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.Airlines.Skip(index).Take(count).AsNoTracking().ToListAsync();

    public async Task<Airline?> GetByCodeAsync(string code) => await context.Airlines.Where(p => p.IataCode == code).AsNoTracking().FirstOrDefaultAsync();

    public async Task<long> GetCountAsync() => await context.Airlines.AsNoTracking().CountAsync();

    public async Task<bool> CheckByCodeAsync(string code) => await context.Airlines.Where(p => p.IataCode == code).AsNoTracking().AnyAsync();
    
    public async Task<bool> AddAsync(Airline obj)
    {
        await context.Airlines.AddAsync(obj);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(Airline obj)
    {
        context.Update(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var dbObj = await context.Airlines.Where(p => p.IataCode == code).FirstOrDefaultAsync();
        if (dbObj == null) return false;
        
        context.Airlines.Remove(dbObj);
        await context.SaveChangesAsync();
        return true;
    }
}