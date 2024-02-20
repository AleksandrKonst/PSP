using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FlightRepository(PSPContext context) : IFlightRepository
{
    public async Task<List<Flight>> GetAllForClientAsync(string arrivePlaceName, string departPlaceName, DateTime date)
    {
        var flights = await context.Flights.Where(f => f.DepartDatetimePlan.Date == date.Date || f.DepartDatetimePlan.Date == date.Date.AddDays(1))
            .Include(f => f.ArrivePlaceNavigation)
                .ThenInclude(f => f.CityIataCodeNavigation)
            .Include(f => f.DepartPlaceNavigation)
                .ThenInclude(f => f.CityIataCodeNavigation)
            .Include(f => f.AirlineCodeNavigation)
            .Include(f => f.FareCodeNavigation)
            .ToListAsync();

        return flights;
    }
}