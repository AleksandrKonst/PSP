using Domain.Models;

namespace Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface IFareRepository : ICrudRepository<Fare, string>
{
    
}