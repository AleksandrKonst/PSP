using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.FlightRepositories;

public class FlightRepository(PSPContext context) : IFlightRepository
{
    public async Task<Flight?> GetByCodeAsync(long code) => await context.Flights.Where(p => p.Code == code).FirstOrDefaultAsync();
    
    public async Task<bool> CheckByCodeAsync(long code) => await context.Flights.Where(f => f.Code == code).AnyAsync();

    public async Task<bool> AddAsync(Flight flight)
    {
        var newFlight = await context.Flights.AddAsync(flight);
        await context.SaveChangesAsync();
        return true;
    }
}