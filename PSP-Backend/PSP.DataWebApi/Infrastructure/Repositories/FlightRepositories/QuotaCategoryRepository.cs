using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FlightRepositories;

public class QuotaCategoryRepository(PSPContext context) : IQuotaCategoryRepository
{
    public async Task<List<QuotaCategory>> GetAllAsync() => await context.QuotaCategories.ToListAsync();
}