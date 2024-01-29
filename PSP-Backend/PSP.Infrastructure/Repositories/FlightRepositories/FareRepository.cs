using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.FlightRepositories;

public class FareRepository(PSPContext context) : IFareRepository
{
    public async Task<Fare?> GetByCodeAsync(string code) => await context.Fare.Where(f => f.Code == code).FirstOrDefaultAsync();
    public async Task<bool> CheckByCodeAsync(string code) => await context.Fare.Where(f => f.Code == code).AnyAsync();
    public async Task<bool> AddAsync(Fare fare)
    {
        var newFare = await context.Fare.AddAsync(fare);
        await context.SaveChangesAsync();
        return true;
    }
}