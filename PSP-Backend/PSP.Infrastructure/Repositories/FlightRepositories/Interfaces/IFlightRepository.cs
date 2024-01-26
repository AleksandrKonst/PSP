using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface IFlightRepository
{
    Task<Flight?> GetByIdAsync(long code);
    Task<bool> CheckByCodeAsync(long id);
    Task<bool> AddAsync(Flight flight);
}