using Domain.Models;

namespace Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface IFlightRepository : ICrudRepository<Flight, long>
{
    
}