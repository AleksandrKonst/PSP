using Microsoft.EntityFrameworkCore;
using PSP.Domain.Exceptions;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.PassengerRepositories;

public class GenderTypeRepository(PSPContext context) : IGenderTypeRepository
{
    public async Task<List<GenderType>> GetAllAsync() => await context.Genders.ToListAsync();

    public async Task<List<GenderType>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.Genders.Skip(index).Take(count).ToListAsync();

    public async Task<GenderType?> GetByCodeAsync(string code) => await context.Genders.Where(p => p.Code == code).FirstOrDefaultAsync();

    public async Task<int> GetCountAsync() => await context.Genders.CountAsync();
    
    public async Task<bool> CheckByCodeAsync(string code) => await context.PassengerTypes.Where(p => p.Code == code).AnyAsync();
}