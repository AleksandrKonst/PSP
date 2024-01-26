using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.FlightRepositories;

public class FlightRepository(PSPContext context) : IFlightRepository
{
    public async Task<Flight?> GetByIdAsync(long code) => await context.DictFlights.Where(p => p.Code == code).FirstOrDefaultAsync();
    
    public async Task<bool> CheckByCodeAsync(long id) => await context.DictFlights.Where(f => f.Code == id).AnyAsync();

    public async Task<bool> AddAsync(Flight flight)
    {
        var newFlight = await context.DictFlights.AddAsync(flight);
        await context.SaveChangesAsync();
        return true;
    }
}