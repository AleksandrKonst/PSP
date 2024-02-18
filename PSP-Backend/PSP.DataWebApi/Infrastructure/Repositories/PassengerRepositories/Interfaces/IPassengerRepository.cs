using Domain.Models;

namespace Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IPassengerRepository
{
    Task<IEnumerable<Passenger>> GetAllAsync();
    Task<IEnumerable<Passenger>> GetPartAsync(int index = 0, int count = Int32.MaxValue);
    Task<Passenger?> GetByIdAsync(long id);
    Task<Passenger?> GetByFullNameWithCouponEventAsync(string name, string surname,
        string? patronymic, string gender, DateOnly birthdate, List<int> year);
    Task<long> GetCountAsync();
    Task<long> GetIdByFullNameAsync(string name, string surname,
        string? patronymic, string gender, DateOnly birthdate);
    
    Task<bool> CheckByFullNameAsync(string name, string surname,
        string? patronymic, string gender, DateOnly birthdate);
    Task<bool> CheckByIdAsync(long id);

    Task<bool> AddAsync(Passenger passenger);
    Task<bool> UpdateAsync(Passenger passenger);
    Task<bool> DeleteAsync(long id);
}