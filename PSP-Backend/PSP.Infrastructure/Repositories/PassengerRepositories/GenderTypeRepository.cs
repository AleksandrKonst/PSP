using Microsoft.EntityFrameworkCore;
using PSP.Domain.Exceptions;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.PassengerRepositories;

public class GenderTypeRepository(PSPContext context) : IGenderTypeRepository
{
    public async Task<List<GenderType>> GetAllAsync() => await context.DictGenders.ToListAsync();

    public async Task<List<GenderType>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.DictGenders.Skip(index).Take(count).ToListAsync();

    public async Task<GenderType?> GetByIdAsync(string code) => await context.DictGenders.Where(p => p.Code == code).FirstOrDefaultAsync();

    public async Task<int> GetCountAsync() => await context.DictGenders.CountAsync();
    
    public async Task<bool> CheckByCodeAsync(string code) => await context.DictPassengerTypes.Where(p => p.Code == code).AnyAsync();
}