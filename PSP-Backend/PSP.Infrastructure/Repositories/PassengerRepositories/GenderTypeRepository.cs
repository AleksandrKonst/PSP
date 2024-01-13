using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Exceptions;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.PassengerRepositories;

public class GenderTypeRepository(PSPContext context) : IGenderTypeRepository
{
    public IQueryable<GenderType> GetAll() => context.DictGenders;

    public async Task<GenderType> GetByIdAsync(string code)
    {
        var genderType = await context.DictGenders.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (genderType == null) throw new ResponseException("Пол не найден", "PPC-000001");
        return genderType;
    }
}