using Microsoft.EntityFrameworkCore;
using PSP.Domain.Exceptions;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.PassengerRepositories;

public class PassengerRepository(PSPContext context) : IPassengerRepository
{
    public Task<List<Passenger>> GetAllAsync() => context.DataPassengers.ToListAsync();
    
    public Task<List<Passenger>> GetPartAsync(int index, int count) => context.DataPassengers.Skip(index).Take(count).ToListAsync();

    public async Task<Passenger> GetByIdAsync(int id)
    {
        var passenger = await context.DataPassengers.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (passenger == null) throw new ResponseException("Пассажир не найден", "PPC-000001");
        return passenger;
    }
    
    public async Task<bool> CheckAsync(string name, string surname, 
        string patronymic, DateOnly birthdate)
    {
        var result = await context.DataPassengers
            .Where(p => p.Name == name && 
                        p.Surname == surname && 
                        p.Patronymic == patronymic && 
                        p.Birthdate == birthdate).AnyAsync();
        return result;
    }
    
    public async Task<Passenger> GetByIdWithQuotaCountAsync(string name, string surname, 
        string patronymic, DateOnly birthdate, List<int> year)
    {
        var passenger = await context.DataPassengers
            .Where(p => p.Name == name && 
                        p.Surname == surname && 
                        p.Patronymic == patronymic && 
                        p.Birthdate == birthdate)
            .Include(p => p.ConPassengerQuotaCounts
                .Where(pq => year.Contains(pq.QuotaYear)))
            .FirstOrDefaultAsync();
        
        //if (passenger == null) throw new ResponseException("Пассажир не найден", "PPC-000001");
        return passenger;
    }

    public Task<int> GetCountAsync() => context.DataPassengers.CountAsync();

    public async Task<bool> UpdateAsync(Passenger passenger)
    {
        var result = await context.DataPassengers.AnyAsync(p => p.Id == passenger.Id);
        if (!result) throw new ResponseException("Пассажир не найден", "PPC-000001");
        
        var updatePassenger = context.Update(passenger);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddAsync(Passenger passenger)
    {
        var result = await context.DataPassengers.AnyAsync(p => p.Id == passenger.Id);
        if (result) throw new ResponseException("Пассажир уже существует", "PPC-000002");

        var newPassenger = await context.DataPassengers.AddAsync(passenger);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dbPassenger = await context.DataPassengers.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (dbPassenger == null) throw new ResponseException("Пассажир не найден", "PPC-000001");
        
        context.DataPassengers.Remove(dbPassenger);
        await context.SaveChangesAsync();
        return true;
    }
}