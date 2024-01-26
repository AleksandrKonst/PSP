using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface IFareRepository
{
    Task<Fare?> GetByIdAsync(string code);

    Task<bool> AddAsync(Fare fare);
}