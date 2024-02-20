using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class QuotaCategoryRepository(PSPContext context) : IQuotaCategoryRepository
{
    public async Task<IEnumerable<QuotaCategory>> GetAllAsync() => await context.QuotaCategories.ToListAsync();

    public async Task<IEnumerable<QuotaCategory>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.QuotaCategories.Skip(index).Take(count).ToListAsync();

    public async Task<QuotaCategory?> GetByCodeAsync(string code) => await context.QuotaCategories.Where(p => p.Code == code).FirstOrDefaultAsync();

    public async Task<long> GetCountAsync() => await context.QuotaCategories.CountAsync();

    public async Task<bool> CheckByCodeAsync(string code) => await context.QuotaCategories.Where(p => p.Code == code).AnyAsync();
    
    public async Task<bool> AddAsync(QuotaCategory obj)
    {
        await context.QuotaCategories.AddAsync(obj);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(QuotaCategory obj)
    {
        context.Update(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var dbObj = await context.QuotaCategories.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (dbObj == null) return false;
        
        context.QuotaCategories.Remove(dbObj);
        await context.SaveChangesAsync();
        return true;
    }
}