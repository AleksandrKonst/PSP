using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSP_Data_Service.Passenger_Context.DTO;
using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;
using PSP_Data_Service.Passenger_Context.Services.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Services;

public class PassengerQuotaCountService(IPassengerQuotaCountRepository repository, IMapper mapper) : IPassengerQuotaCountService
{
    public async Task<IEnumerable<PassengerQuotaCountDTO>> GetPassengerQuotaCountsAsync(int index, int count) => mapper.Map<IEnumerable<PassengerQuotaCountDTO>>(await repository.GetAll().Skip(index).Take(count).ToListAsync());

    public async Task<int> GetPassengerQuotaCountLenghtAsync() => await repository.GetAll().CountAsync();

    public async Task<PassengerQuotaCountDTO> GetPassengerQuotaCountByIdAsync(long passengerId, string quotaCategory, string year) => mapper.Map<PassengerQuotaCountDTO>(await repository.GetByIdAsync(passengerId, quotaCategory, year));

    public async Task<bool> AddPassengerQuotaCountAsync(PassengerQuotaCountDTO dto) =>  await repository.Add(mapper.Map<PassengerQuotaCount>(dto));
    
    public async Task<bool> UpdatePassengerQuotaCountAsync(PassengerQuotaCountDTO dto) =>  await repository.Update(mapper.Map<PassengerQuotaCount>(dto));
    
    public async Task<bool> DeletePassengerQuotaCountAsync(long id) =>  await repository.Delete(id);
}