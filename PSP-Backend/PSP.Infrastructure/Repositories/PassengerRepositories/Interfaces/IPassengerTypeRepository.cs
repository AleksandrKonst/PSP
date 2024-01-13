using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IPassengerTypeRepository
{
    IQueryable<PassengerType> GetAll();
    Task<PassengerType> GetByIdAsync(string code);
    Task<bool> Add(PassengerType passengerType);
    Task<bool> Update(PassengerType passengerType);
    Task<bool> Delete(string code);
}