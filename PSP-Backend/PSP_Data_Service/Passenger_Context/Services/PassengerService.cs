using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSP_Data_Service.Passenger_Context.DTO;
using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;
using PSP_Data_Service.Passenger_Context.Services.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Services;

public class PassengerService(IPassengerRepository repository, IMapper mapper) : IPassengerService
{
    public async Task<IEnumerable<PassengerDTO>> GetPassengersAsync(int index, int count) => mapper.Map<IEnumerable<PassengerDTO>>(await repository.GetAll().Skip(index).Take(count).ToListAsync());
    
    public async Task<int> GetPassengersCountAsync() => await repository.GetAll().CountAsync();
    
    public async Task<PassengerDTO> GetPassengerByIdAsync(int id) => mapper.Map<PassengerDTO>(await repository.GetByIdAsync(id));
    
    public async Task<bool> AddPassenger(PassengerDTO dto) =>  await repository.Add(mapper.Map<Passenger>(dto));
    
    public async Task<bool> UpdatePassenger(PassengerDTO dto) =>  await repository.Update(mapper.Map<Passenger>(dto));
    
    public async Task<bool> DeletePassenger(int id) =>  await repository.Delete(id);
}