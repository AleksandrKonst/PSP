using Microsoft.EntityFrameworkCore;
using PSP_Data_Service.Passenger_Context.Infrastructure.Exceptions;
using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Repositories;

public class PassengerRepository(PassengerDataContext context) : IPassengerRepository
{
    public IQueryable<Passenger> GetAll() => context.DataPassengers;

    public async Task<Passenger> GetByIdAsync(int id)
    {
        var passenger = await context.DataPassengers.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (passenger == null) throw new ResponseException("Пассажир не найден", "PPC-000001");
        return passenger;
    }
    
    public async Task<bool> Update(Passenger passenger)
    {
        var dbPassenger = await context.DataPassengers.AnyAsync(p => p.Id == passenger.Id);
        if (!dbPassenger) throw new ResponseException("Пассажир не найден", "PPC-000001");
        
        var updatePassenger = context.Update(passenger);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Add(Passenger passenger)
    {
        var dbPassenger = await context.DataPassengers.AnyAsync(p => p.Id == passenger.Id);
        if (dbPassenger) throw new ResponseException("Пассажир уже существует", "PPC-000002");

        var newPassenger = await context.DataPassengers.AddAsync(passenger);
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