using Microsoft.EntityFrameworkCore;
using PSP_Data_Service.Passenger_Context.Infrastructure.Exceptions;
using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Repositories;

public class PassengerQuotaCountRepository(PassengerDataContext context) : IPassengerQuotaCountRepository
{
    public IQueryable<PassengerQuotaCount> GetAll() => context.ConPassengerQuotaCounts;
    public IQueryable<PassengerQuotaCount> GetByIdAsync(long passengerId, string quotaCategory, string year)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PassengerQuotaCount> GetByIdAsync(int id) => context.ConPassengerQuotaCounts.Where(p => p.PassengerId == id);
    
    public async Task<bool> Update(PassengerQuotaCount passengerQuotaCount)
    {
        var dbPassenger = await context.ConPassengerQuotaCounts.AnyAsync(p => p.PassengerId == passengerQuotaCount.PassengerId);
        if (!dbPassenger) throw new ResponseException("Пассажир не найден", "PPC-000001");
        
        var updatePassenger = context.Update(passengerQuotaCount);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Add(PassengerQuotaCount passengerQuotaCount)
    {
        var dbPassenger = await context.ConPassengerQuotaCounts.AnyAsync(p => p.PassengerId == passengerQuotaCount.PassengerId);
        if (dbPassenger) throw new ResponseException("Пассажир уже существует", "PPC-000002");

        var newPassenger = await context.ConPassengerQuotaCounts.AddAsync(passengerQuotaCount);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var dbPassenger = await context.DataPassengers.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (dbPassenger == null) throw new ResponseException("Пассажир не найден", "PPC-000001");
        
        context.DataPassengers.Remove(dbPassenger);
        await context.SaveChangesAsync();
        return true;
    }
}