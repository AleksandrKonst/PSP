using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface ISubsidizedRouteRepository
{
    Task<List<SubsidizedRoute>> GetAllByAppendixAsync(short appendix);
}