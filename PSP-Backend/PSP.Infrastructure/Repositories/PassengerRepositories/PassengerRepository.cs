using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.PassengerRepositories;

public class PassengerRepository(PSPContext context) : IPassengerRepository
{
    public async Task<List<Passenger>> GetAllAsync() => await context.DataPassengers.ToListAsync();
    
    public async Task<List<Passenger>> GetPartAsync(int index, int count) => await context.DataPassengers.Skip(index).Take(count).ToListAsync();

    public async Task<Passenger?> GetByIdAsync(int id) => await context.DataPassengers.Where(p => p.Id == id).FirstOrDefaultAsync();
    
    public async Task<Passenger?> GetByIdWithCouponEventAsync(string name, string surname, 
        string patronymic, DateOnly birthdate, List<int> year)
    {
        var passenger = await context.DataPassengers
            .Where(p => p.Name == name && 
                        p.Surname == surname && 
                        p.Patronymic == patronymic && 
                        p.Birthdate == birthdate)
            .Include(p => p.DataCouponEvents.Where(dc => year.Contains(dc.OperationDatetimeUtc.Year)))
            .FirstOrDefaultAsync();
        
        return passenger;
    }
    
    public async Task<int> GetCountAsync() => await context.DataPassengers.CountAsync();
    
    public async Task<bool> CheckByFullNameAsync(string name, string surname, 
        string patronymic, DateOnly birthdate)
    {
        var result = await context.DataPassengers
            .Where(p => p.Name == name && 
                        p.Surname == surname && 
                        p.Patronymic == patronymic && 
                        p.Birthdate == birthdate).AnyAsync();
        return result;
    }
    
    public async Task<bool> CheckByIdAsync(long id) => await context.DataPassengers.Where(p => p.Id == id).AnyAsync();

    public async Task<bool> AddAsync(Passenger passenger)
    {
        var newPassenger = await context.DataPassengers.AddAsync(passenger);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(Passenger passenger)
    {
        var updatePassenger = context.Update(passenger);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dbPassenger = await context.DataPassengers.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (dbPassenger == null) return false;
        
        context.DataPassengers.Remove(dbPassenger);
        await context.SaveChangesAsync();
        return true;
    }
}