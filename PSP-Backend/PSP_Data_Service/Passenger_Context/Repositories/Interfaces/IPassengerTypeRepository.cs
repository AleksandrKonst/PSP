using PSP_Data_Service.Passenger_Context.Models;

namespace PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

public interface IPassengerTypeRepository
{
    IQueryable<PassengerType> GetAll();
    Task<PassengerType> GetByIdAsync(string code);
    Task<bool> Add(PassengerType passengerType);
    Task<bool> Update(PassengerType passengerType);
    Task<bool> Delete(string code);
}