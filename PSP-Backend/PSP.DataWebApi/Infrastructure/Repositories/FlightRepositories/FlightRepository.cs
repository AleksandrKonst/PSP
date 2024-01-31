using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

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