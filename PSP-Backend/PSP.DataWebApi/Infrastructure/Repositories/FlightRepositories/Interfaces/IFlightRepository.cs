using Domain.Models;

namespace Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface IFlightRepository
{
    Task<Flight?> GetByCodeAsync(long code);
    Task<bool> CheckByCodeAsync(long code);
    Task<bool> AddAsync(Flight flight);
}