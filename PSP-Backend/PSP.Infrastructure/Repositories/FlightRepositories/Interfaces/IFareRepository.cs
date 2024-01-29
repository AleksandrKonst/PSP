using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface IFareRepository
{
    Task<Fare?> GetByCodeAsync(string code);
    Task<bool> CheckByCodeAsync(string code);
    Task<bool> AddAsync(Fare fare);
}