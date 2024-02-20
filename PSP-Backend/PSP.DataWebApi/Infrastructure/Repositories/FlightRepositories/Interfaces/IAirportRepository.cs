using Domain.Models;

namespace Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface IAirportRepository : ICrudRepository<Airport, string>
{
    
}