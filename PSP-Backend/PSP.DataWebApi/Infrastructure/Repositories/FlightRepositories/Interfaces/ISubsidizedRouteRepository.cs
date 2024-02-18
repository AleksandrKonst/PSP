using Domain.Models;

namespace Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface ISubsidizedRouteRepository : ICrudRepository<SubsidizedRoute, long>
{
    Task<List<SubsidizedRoute>> GetAllByAppendixAsync(short appendix);
}