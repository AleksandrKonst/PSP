using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.PassengerRepositories;

public class PassengerTypeRepository(PSPContext context) : IPassengerTypeRepository
{
    public async Task<IEnumerable<PassengerType>> GetAllAsync() => await context.PassengerTypes.ToListAsync();
    
    public async Task<IEnumerable<PassengerType>> GetPartAsync(int index, int count) => await context.PassengerTypes.Skip(index).Take(count).ToListAsync();

    public async Task<PassengerType?> GetByCodeAsync(string code) => await context.PassengerTypes.Where(p => p.Code == code).FirstOrDefaultAsync();

    public async Task<int> GetCountAsync() => await context.PassengerTypes.CountAsync();

    public async Task<bool> CheckByCodeAsync(string code) => await context.PassengerTypes.Where(p => p.Code == code).AnyAsync();
    
    public async Task<bool> AddAsync(PassengerType passengerType)
    {
        await context.PassengerTypes.AddAsync(passengerType);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(PassengerType passengerType)
    {
        context.Update(passengerType);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var dbPassengerType = await context.PassengerTypes.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (dbPassengerType == null) return false;
        
        context.PassengerTypes.Remove(dbPassengerType);
        await context.SaveChangesAsync();
        return true;
    }
}