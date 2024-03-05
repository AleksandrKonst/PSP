using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.PassengerRepositories;

public class GenderTypeRepository(PSPContext context) : IGenderTypeRepository
{
    public async Task<IEnumerable<GenderType>> GetAllAsync() => await context.Genders.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<GenderType>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.Genders.Skip(index).Take(count).AsNoTracking().ToListAsync();

    public async Task<GenderType?> GetByCodeAsync(string code) => await context.Genders.Where(p => p.Code == code).AsNoTracking().FirstOrDefaultAsync();

    public async Task<int> GetCountAsync() => await context.Genders.AsNoTracking().CountAsync();
    
    public async Task<bool> CheckByCodeAsync(string code) => await context.PassengerTypes.Where(p => p.Code == code).AsNoTracking().AnyAsync();
}