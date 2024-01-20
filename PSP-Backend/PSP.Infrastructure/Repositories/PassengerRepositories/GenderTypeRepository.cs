using Microsoft.EntityFrameworkCore;
using PSP.Domain.Exceptions;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.PassengerRepositories;

public class GenderTypeRepository(PSPContext context) : IGenderTypeRepository
{
    public IQueryable<GenderType> GetAll() => context.DictGenders;

    public Task<List<GenderType>> GetAllAsync() => context.DictGenders.ToListAsync();

    public Task<List<GenderType>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => context.DictGenders.Skip(index).Take(count).ToListAsync();

    public async Task<GenderType> GetByIdAsync(string code)
    {
        var genderType = await context.DictGenders.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (genderType == null) throw new ResponseException("Пол не найден", "PPC-000001");
        return genderType;
    }

    public Task<int> GetCountAsync() => context.DictGenders.CountAsync();
}