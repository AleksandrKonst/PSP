using Microsoft.EntityFrameworkCore;
using PSP_Data_Service.Passenger_Context.Infrastructure.Exceptions;
using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Repositories;

public class GenderTypeRepository(PassengerDataContext context) : IGenderTypeRepository
{
    public IQueryable<GenderType> GetAll() => context.DictGenders;

    public async Task<GenderType> GetByIdAsync(string code)
    {
        var genderType = await context.DictGenders.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (genderType == null) throw new ResponseException("Пол не найден", "PPC-000001");
        return genderType;
    }
}