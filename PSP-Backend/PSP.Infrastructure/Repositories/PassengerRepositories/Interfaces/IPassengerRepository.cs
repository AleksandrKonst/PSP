using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IPassengerRepository
{
    Task<List<Passenger>> GetAllAsync();
    Task<List<Passenger>> GetPartAsync(int index = 0, int count = Int32.MaxValue);
    Task<Passenger?> GetByIdAsync(long id);
    Task<Passenger?> GetByFullNameWithCouponEventAsync(string name, string surname,
        string? patronymic, DateOnly birthdate, List<int> year);
    Task<int> GetCountAsync();
    Task<bool> CheckByFullNameAsync(string name, string surname,
        string? patronymic, DateOnly birthdate);
    Task<bool> CheckByIdAsync(long id);
    Task<bool> AddAsync(Passenger passenger);
    Task<bool> UpdateAsync(Passenger passenger);
    Task<bool> DeleteAsync(int id);
}