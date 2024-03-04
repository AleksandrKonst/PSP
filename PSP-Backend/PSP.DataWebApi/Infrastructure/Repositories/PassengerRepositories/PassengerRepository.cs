using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.PassengerRepositories;

public class PassengerRepository(PSPContext context) : IPassengerRepository
{
    public async Task<IEnumerable<Passenger>> GetAllAsync() => await context.Passengers.AsNoTracking().ToListAsync();
    
    public async Task<IEnumerable<Passenger>> GetPartAsync(int index, int count) => await context.Passengers.Skip(index).Take(count).AsNoTracking().ToListAsync();

    public async Task<Passenger?> GetByIdAsync(long id) => await context.Passengers.Where(p => p.Id == id).AsNoTracking().FirstOrDefaultAsync();
    
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
            .AsNoTracking()
            .FirstOrDefaultAsync();
        
        return passenger;
    }
    
    public async Task<Passenger?> GetByFullNameWithCouponEventAsync(string name, string surname, 
        string? patronymic, DateOnly birthdate, List<int> year)
    {
        var passenger = await context.Passengers
            .Where(p => p.Name == name && 
                        p.Surname == surname && 
                        p.Patronymic == patronymic && 
                        p.Birthdate == birthdate)
            .Include(p => p.CouponEvents.Where(dc => year.Contains(dc.OperationDatetimeUtc.Year)))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        
        return passenger;
    }
    
    public async Task<Passenger?> GetByFullNameAsync(string name, string surname,
        string? patronymic, DateOnly birthdate) => await context.Passengers.Where(p => p.Name == name &&
            p.Surname == surname &&
            p.Patronymic == patronymic && 
            p.Birthdate == birthdate)
        .AsNoTracking()
        .FirstOrDefaultAsync();
    
    public async Task<long> GetCountAsync() => await context.Passengers.AsNoTracking().CountAsync();

    public async Task<bool> CheckByFullNameAsync(string name, string surname, 
        string? patronymic, string gender, DateOnly birthdate)
    {
        var result = await context.Passengers
            .Where(p => p.Name == name && 
                        p.Surname == surname && 
                        p.Patronymic == patronymic && 
                        p.Birthdate == birthdate && 
                        p.Gender == gender).AsNoTracking().AnyAsync();
        return result;
    }
    
    public async Task<bool> CheckByIdAsync(long id) => await context.Passengers.Where(p => p.Id == id).AsNoTracking().AnyAsync();

    public async Task<bool> AddAsync(Passenger passenger)
    {
        await context.Passengers.AddAsync(passenger);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(Passenger passenger)
    {
        context.Update(passenger);
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