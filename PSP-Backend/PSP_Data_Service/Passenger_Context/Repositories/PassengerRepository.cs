using Microsoft.EntityFrameworkCore;
using PSP_Data_Service.Passenger_Context.Infrastructure.Exceptions;
using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Repositories;

public class PassengerRepository(PassengerDataContext context) : IPassengerRepository
{
    public IQueryable<Passenger> GetAll() => context.DataPassengers;

    public async Task<Passenger?> GetByIdAsync(int id)
    {
        var passenger = await context.DataPassengers.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (passenger == null) throw new ResponseException("Пассажир не найден", "PPC-000001");
        return passenger;
    }
    
    public async Task<bool> Update(Passenger passenger)
    {
        try
        {
            var updatePassenger = context.Update(passenger);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Add(Passenger passenger)
    {
        try
        {
            var dbPassenger = await context.DataPassengers.Where(p => p.Id == passenger.Id).FirstOrDefaultAsync();
            if (dbPassenger != null) return false;

            var newPassenger = await context.DataPassengers.AddAsync(passenger);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var dbPassenger = await context.DataPassengers.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (dbPassenger != null) context.DataPassengers.Remove(dbPassenger);
    
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}