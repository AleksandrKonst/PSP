using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class CityRepository(PSPContext context) : ICityRepository
{
    public async Task<IEnumerable<City>> GetAllAsync() => await context.Cities.ToListAsync();

    public async Task<IEnumerable<City>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.Cities.Skip(index).Take(count).ToListAsync();

    public async Task<City?> GetByCodeAsync(string code) => await context.Cities.Where(p => p.IataCode == code).FirstOrDefaultAsync();

    public async Task<long> GetCountAsync() => await context.Cities.CountAsync();

    public async Task<bool> CheckByCodeAsync(string code) => await context.Cities.Where(p => p.IataCode == code).AnyAsync();
    
    public async Task<bool> AddAsync(City obj)
    {
        await context.Cities.AddAsync(obj);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(City obj)
    {
        context.Update(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var dbObj = await context.Cities.Where(p => p.IataCode == code).FirstOrDefaultAsync();
        if (dbObj == null) return false;
        
        context.Cities.Remove(dbObj);
        await context.SaveChangesAsync();
        return true;
    }
}