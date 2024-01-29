using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.PassengerRepositories;

public class PassengerRepository(PSPContext context) : IPassengerRepository
{
    public async Task<List<Passenger>> GetAllAsync() => await context.Passengers.ToListAsync();
    
    public async Task<List<Passenger>> GetPartAsync(int index, int count) => await context.Passengers.Skip(index).Take(count).ToListAsync();

    public async Task<Passenger?> GetByIdAsync(long id) => await context.Passengers.Where(p => p.Id == id).FirstOrDefaultAsync();
    
    public async Task<Passenger?> GetByFullNameWithCouponEventAsync(string name, string surname, 
        string? patronymic, string gender, DateOnly birthdate, List<int> year)
    {
        var passenger = await context.Passengers
            .Where(p => p.Name == name && 
                        p.Surname == surname && 
                        p.Patronymic == patronymic && 
                        p.Birthdate == birthdate && 
                        p.Gender == gender)
            .Include(p => p.CouponEvents.Where(dc => year.Contains(dc.OperationDatetimeUtc.Year)))
            .FirstOrDefaultAsync();
        
        return passenger;
    }
    
    public async Task<int> GetCountAsync() => await context.Passengers.CountAsync();

    public async Task<long> GetIdByFullNameAsync(string name, string surname,
        string? patronymic, string gender, DateOnly birthdate) => await context.Passengers.Where(p => p.Name == name &&
            p.Surname == surname &&
            p.Patronymic == patronymic && 
            p.Birthdate == birthdate && 
            p.Gender == gender)
        .Select(p => p.Id).FirstOrDefaultAsync();

    public async Task<bool> CheckByFullNameAsync(string name, string surname, 
        string? patronymic, string gender, DateOnly birthdate)
    {
        var result = await context.Passengers
            .Where(p => p.Name == name && 
                        p.Surname == surname && 
                        p.Patronymic == patronymic && 
                        p.Birthdate == birthdate && 
                        p.Gender == gender).AnyAsync();
        return result;
    }
    
    public async Task<bool> CheckByIdAsync(long id) => await context.Passengers.Where(p => p.Id == id).AnyAsync();

    public async Task<bool> AddAsync(Passenger passenger)
    {
        var newPassenger = await context.Passengers.AddAsync(passenger);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(Passenger passenger)
    {
        var updatePassenger = context.Update(passenger);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var dbPassenger = await context.Passengers.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (dbPassenger == null) return false;
        
        context.Passengers.Remove(dbPassenger);
        await context.SaveChangesAsync();
        return true;
    }
}