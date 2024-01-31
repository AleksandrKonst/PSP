using Domain.Models;

namespace Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface ISubsidizedRouteRepository
{
    Task<List<SubsidizedRoute>> GetAllByAppendixAsync(short appendix);
}