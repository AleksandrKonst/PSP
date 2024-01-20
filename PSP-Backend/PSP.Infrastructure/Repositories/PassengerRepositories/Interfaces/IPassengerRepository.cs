using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IPassengerRepository
{
    Task<List<Passenger>> GetAllAsync();
    Task<List<Passenger>> GetPartAsync(int index = 0, int count = Int32.MaxValue);
    Task<Passenger> GetByIdAsync(int id);

    Task<bool> CheckAsync(string name, string surname,
        string patronymic, DateOnly birthdate);
    Task<Passenger> GetByIdWithQuotaCountAsync(string name, string surname,
        string patronymic, DateOnly birthdate, List<int> year);
    Task<int> GetCountAsync();
    Task<bool> AddAsync(Passenger passenger);
    Task<bool> UpdateAsync(Passenger passenger);
    Task<bool> DeleteAsync(int id);
}