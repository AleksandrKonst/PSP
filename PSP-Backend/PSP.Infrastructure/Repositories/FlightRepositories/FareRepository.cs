using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.FlightRepositories;

public class FareRepository(PSPContext context) : IFareRepository
{
    public async Task<Fare?> GetByIdAsync(string code) => await context.DictFare.Where(f => f.Code == code).FirstOrDefaultAsync();
    
    public async Task<bool> AddAsync(Fare fare)
    {
        var newFare = await context.DictFare.AddAsync(fare);
        await context.SaveChangesAsync();
        return true;
    }
}