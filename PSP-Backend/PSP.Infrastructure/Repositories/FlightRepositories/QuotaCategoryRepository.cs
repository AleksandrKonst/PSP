using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.FlightRepositories;

public class QuotaCategoryRepository(PSPContext context) : IQuotaCategoryRepository
{
    public async Task<List<QuotaCategory>> GetAllAsync() => await context.QuotaCategories.ToListAsync();
}