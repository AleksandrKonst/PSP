using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class SubsidizedRouteRepository(PSPContext context) : ISubsidizedRouteRepository
{
    public async Task<List<SubsidizedRoute>> GetAllByAppendixAsync(short appendix) => await context.SubsidizedRoutes.Where(s => s.Appendix == appendix).AsNoTracking().ToListAsync();
    
    public async Task<IEnumerable<SubsidizedRoute>> GetAllAsync() => await context.SubsidizedRoutes.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<SubsidizedRoute>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.SubsidizedRoutes.Skip(index).Take(count).AsNoTracking().ToListAsync();

    public async Task<SubsidizedRoute?> GetByCodeAsync(long code) => await context.SubsidizedRoutes.Where(p => p.Id == code).AsNoTracking().FirstOrDefaultAsync();

    public async Task<long> GetCountAsync() => await context.SubsidizedRoutes.AsNoTracking().CountAsync();

    public async Task<bool> CheckByCodeAsync(long code) => await context.SubsidizedRoutes.Where(p => p.Id == code).AsNoTracking().AnyAsync();
    
    public async Task<bool> AddAsync(SubsidizedRoute obj)
    {
        await context.SubsidizedRoutes.AddAsync(obj);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(SubsidizedRoute obj)
    {
        context.Update(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long code)
    {
        var dbObj = await context.SubsidizedRoutes.Where(p => p.Id == code).FirstOrDefaultAsync();
        if (dbObj == null) return false;
        
        context.SubsidizedRoutes.Remove(dbObj);
        await context.SaveChangesAsync();
        return true;
    }
}