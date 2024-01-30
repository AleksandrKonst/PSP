using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.FlightRepositories;

public class FlightRepository(PSPContext context) : IFlightRepository
{
    public async Task<List<Flight>> GetAllForClientAsync(string arrivePlaceName, string departPlaceName, DateTime date)
    {
        var flights = await context.Flights.Where(f =>
                f.ArrivePlaceNavigation.CityIataCodeNavigation.Name == arrivePlaceName &&
                f.DepartPlaceNavigation.CityIataCodeNavigation.Name == departPlaceName && 
                f.DepartDatetimePlan.Date == date.Date)
            .OrderBy(f => f.ArriveDatetimePlan - f.DepartDatetimePlan)
            .ThenBy(f => f.FareCodeNavigation.Amount)
            .Include(f => f.ArrivePlaceNavigation)
            .Include(f => f.DepartPlaceNavigation)
            .Include(f => f.AirlineCodeNavigation)
            .Include(f => f.FareCodeNavigation)
            .ToListAsync();

        return flights;
    }

    public async Task<Flight?> GetByCodeAsync(long code) => await context.Flights.Where(p => p.Code == code).FirstOrDefaultAsync();
    
    public async Task<bool> CheckByCodeAsync(long code) => await context.Flights.Where(f => f.Code == code).AnyAsync();

    public async Task<bool> AddAsync(Flight flight)
    {
        var newFlight = await context.Flights.AddAsync(flight);
        await context.SaveChangesAsync();
        return true;
    }
}