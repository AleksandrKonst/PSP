using Domain.Models;

namespace Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IPassengerTypeRepository
{
    Task<IEnumerable<PassengerType>> GetAllAsync();
    Task<IEnumerable<PassengerType>> GetPartAsync(int index = 0, int count = Int32.MaxValue);
    Task<PassengerType?> GetByCodeAsync(string code);
    Task<int> GetCountAsync();
    Task<bool> CheckByCodeAsync(string code);
    Task<bool> AddAsync(PassengerType passengerType);
    Task<bool> UpdateAsync(PassengerType passengerType);
    Task<bool> DeleteAsync(string code);
}