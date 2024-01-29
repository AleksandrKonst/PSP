using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.PassengerRepositories;

public class PassengerTypeRepository(PSPContext context) : IPassengerTypeRepository
{
    public async Task<List<PassengerType>> GetAllAsync() => await context.PassengerTypes.ToListAsync();
    
    public async Task<List<PassengerType>> GetPartAsync(int index, int count) => await context.PassengerTypes.Skip(index).Take(count).ToListAsync();

    public async Task<PassengerType?> GetByCodeAsync(string code) => await context.PassengerTypes.Where(p => p.Code == code).FirstOrDefaultAsync();

    public async Task<int> GetCountAsync() => await context.PassengerTypes.CountAsync();

    public async Task<bool> CheckByCodeAsync(string code) => await context.PassengerTypes.Where(p => p.Code == code).AnyAsync();
    
    public async Task<bool> AddAsync(PassengerType passengerType)
    {
        var newPassengerType = await context.PassengerTypes.AddAsync(passengerType);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(PassengerType passengerType)
    {
        var updatePassengerType = context.Update(passengerType);
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