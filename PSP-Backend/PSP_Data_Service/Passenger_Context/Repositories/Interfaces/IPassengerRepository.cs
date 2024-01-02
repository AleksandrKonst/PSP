using PSP_Data_Service.Passenger_Context.Models;

namespace PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

public interface IPassengerRepository
{
    IQueryable<Passenger> GetAll();
    Task<Passenger> GetByIdAsync(int id);
    Task<bool> Add(Passenger passenger);
    Task<bool> Update(Passenger passenger);
    Task<bool> Delete(int id);
}