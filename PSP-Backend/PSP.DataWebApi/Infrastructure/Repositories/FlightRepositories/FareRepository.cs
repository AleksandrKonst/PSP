using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class FareRepository(PSPContext context) : IFareRepository
{
    public async Task<IEnumerable<Fare>> GetAllAsync() => await context.Fare.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<Fare>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.Fare.Skip(index).Take(count).AsNoTracking().ToListAsync();

    public async Task<long> GetCountAsync() => await context.Fare.AsNoTracking().CountAsync();
    
    public async Task<bool> AddAsync(Fare obj)
    {
        await context.Fare.AddAsync(obj);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(Fare obj)
    {
        context.Update(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var dbObj = await context.Fare.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (dbObj == null) return false;
        
        context.Fare.Remove(dbObj);
        await context.SaveChangesAsync();
        return true;
    }
    public async Task<Fare?> GetByCodeAsync(string code) => await context.Fare.Where(f => f.Code == code).AsNoTracking().FirstOrDefaultAsync();
    public async Task<bool> CheckByCodeAsync(string code) => await context.Fare.Where(f => f.Code == code).AsNoTracking().AnyAsync();
}