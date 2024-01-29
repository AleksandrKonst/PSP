using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IGenderTypeRepository
{
    Task<List<GenderType>> GetAllAsync();
    Task<List<GenderType>> GetPartAsync(int index = 0, int count = Int32.MaxValue);
    Task<GenderType?> GetByCodeAsync(string code);
    Task<int> GetCountAsync();
    Task<bool> CheckByCodeAsync(string code);
}