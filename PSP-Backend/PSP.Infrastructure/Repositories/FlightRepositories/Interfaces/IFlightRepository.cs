using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface IFlightRepository
{
    Task<Flight?> GetByCodeAsync(long code);
    Task<bool> CheckByCodeAsync(long code);
    Task<bool> AddAsync(Flight flight);
}