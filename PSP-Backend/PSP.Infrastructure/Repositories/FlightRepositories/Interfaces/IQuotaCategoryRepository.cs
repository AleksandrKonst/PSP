using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

public interface IQuotaCategoryRepository
{
    Task<List<QuotaCategory>> GetAllAsync();
}