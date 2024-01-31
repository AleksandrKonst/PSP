using Domain.Models;

namespace Infrastructure.Repositories.Interfaces;

public interface IFlightRepository
{
    Task<List<Flight>> GetAllForClientAsync(string arrivePlaceName, string departPlaceName, DateTime date);
}