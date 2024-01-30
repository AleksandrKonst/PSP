using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.FlightRepositories;

public class SubsidizedRouteRepository(PSPContext context) : ISubsidizedRouteRepository
{
    public async Task<List<SubsidizedRoute>> GetAllByAppendixAsync(short appendix) => await context.SubsidizedRoutes.Where(s => s.Appendix == appendix).ToListAsync();
}