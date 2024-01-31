using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

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
}