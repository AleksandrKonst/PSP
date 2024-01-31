using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class SubsidizedRouteRepository(PSPContext context) : ISubsidizedRouteRepository
{
    public async Task<List<SubsidizedRoute>> GetAllByAppendixAsync(short appendix) => await context.SubsidizedRoutes.Where(s => s.Appendix == appendix).ToListAsync();
}