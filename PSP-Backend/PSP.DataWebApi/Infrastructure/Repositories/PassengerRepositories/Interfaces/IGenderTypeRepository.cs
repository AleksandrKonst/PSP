using Domain.Models;

namespace Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IGenderTypeRepository
{
    Task<IEnumerable<GenderType>> GetAllAsync();
    Task<IEnumerable<GenderType>> GetPartAsync(int index = 0, int count = Int32.MaxValue);
    Task<GenderType?> GetByCodeAsync(string code);
    Task<int> GetCountAsync();
    Task<bool> CheckByCodeAsync(string code);
}