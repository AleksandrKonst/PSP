using Microsoft.EntityFrameworkCore;
using PSP_Data_Service.Passenger_Context.Infrastructure.Exceptions;
using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Repositories;

public class PassengerTypeRepository(PassengerDataContext context) : IPassengerTypeRepository
{
    public IQueryable<PassengerType> GetAll() => context.DictPassengerTypes;

    public async Task<PassengerType> GetByIdAsync(string code)
    {
        var passengerType = await context.DictPassengerTypes.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (passengerType == null) throw new ResponseException("Тип пассажира не найден", "PPC-000001");
        return passengerType;
    }
    
    public async Task<bool> Update(PassengerType passengerType)
    {
        var dbPassenger = await context.DictPassengerTypes.AnyAsync(p => p.Code == passengerType.Code);
        if (!dbPassenger) throw new ResponseException("Тип пассажира не найден", "PPC-000001");
        
        var updatePassengerType = context.Update(passengerType);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Add(PassengerType passengerType)
    {
        var dbPassenger = await context.DictPassengerTypes.AnyAsync(p => p.Code == passengerType.Code);
        if (dbPassenger) throw new ResponseException("Тип пассажира уже существует", "PPC-000002");

        var newPassengerType = await context.DictPassengerTypes.AddAsync(passengerType);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(string code)
    {
        var dbPassengerType = await context.DictPassengerTypes.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (dbPassengerType == null) throw new ResponseException("Тип пассажира не найден", "PPC-000001");
        
        context.DictPassengerTypes.Remove(dbPassengerType);
        await context.SaveChangesAsync();
        return true;
    }
}