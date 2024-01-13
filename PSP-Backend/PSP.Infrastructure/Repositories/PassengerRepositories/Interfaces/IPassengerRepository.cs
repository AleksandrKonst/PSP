using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IPassengerRepository
{
    IQueryable<Passenger> GetAll();
    Task<Passenger> GetByIdAsync(int id);
    Task<bool> Add(Passenger passenger);
    Task<bool> Update(Passenger passenger);
    Task<bool> Delete(int id);
}