using Microsoft.EntityFrameworkCore;
using PSP.Domain.Exceptions;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.PassengerRepositories;

public class PassengerQuotaCountRepository(PSPContext context) : IPassengerQuotaCountRepository
{
    public Task<List<PassengerQuotaCount>> GetAllAsync() => context.ConPassengerQuotaCounts.ToListAsync();

    public Task<List<PassengerQuotaCount>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => context.ConPassengerQuotaCounts.Skip(index).Take(count).ToListAsync();

    public async Task<PassengerQuotaCount> GetByIdAsync(long passengerId, string quotaCategory, int year) {
        var passengerQuotaCount = await context.ConPassengerQuotaCounts
            .Where(p => p.PassengerId == passengerId && 
                        p.QuotaCategoriesCode == quotaCategory && p.QuotaYear == year)
            .FirstOrDefaultAsync();
        if (passengerQuotaCount == null) throw new ResponseException("Квота не найдена", "PPC-000001");
        return passengerQuotaCount;
    }

    public Task<int> GetCountAsync() => context.ConPassengerQuotaCounts.CountAsync();
    
    public async Task<bool> UpdateAsync(PassengerQuotaCount passengerQuotaCount)
    {
        var result = await context.ConPassengerQuotaCounts.AnyAsync(p => p.PassengerId == passengerQuotaCount.PassengerId);
        if (!result) throw new ResponseException("Квота не найдена", "PPC-000001");
        
        var updatePassenger = context.Update(passengerQuotaCount);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddAsync(PassengerQuotaCount passengerQuotaCount)
    {
        var result = await context.ConPassengerQuotaCounts.AnyAsync(p => p.PassengerId == passengerQuotaCount.PassengerId);
        if (result) throw new ResponseException("Квота уже существует", "PPC-000002");

        var newPassenger = await context.ConPassengerQuotaCounts.AddAsync(passengerQuotaCount);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var dbPassenger = await context.DataPassengers.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (dbPassenger == null) throw new ResponseException("Квота не найдена", "PPC-000001");
        
        context.DataPassengers.Remove(dbPassenger);
        await context.SaveChangesAsync();
        return true;
    }
}