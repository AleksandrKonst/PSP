using Domain.Models;

namespace Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface IQuotaCategoryRepository
{
    Task<List<QuotaCategory>> GetAllAsync();
}