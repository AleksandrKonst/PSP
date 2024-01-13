using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSP.DataWebApi.Passenger_Context.DTO;
using PSP.DataWebApi.Passenger_Context.Services.Interfaces;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataWebApi.Passenger_Context.Services;

public class PassengerTypeService(IPassengerTypeRepository repository, IMapper mapper) : IPassengerTypeService
{
    public async Task<IEnumerable<PassengerTypeDTO>> GetPassengerTypesAsync(int index, int count) => mapper.Map<IEnumerable<PassengerTypeDTO>>(await repository.GetAll().Skip(index).Take(count).ToListAsync());

    public async Task<int> GetPassengerTypesCountAsync() => await repository.GetAll().CountAsync();

    public async Task<PassengerTypeDTO> GetPassengerTypeByIdAsync(string id) => mapper.Map<PassengerTypeDTO>(await repository.GetByIdAsync(id));

    public async Task<bool> AddPassengerTypeAsync(PassengerTypeDTO dto) =>  await repository.Add(mapper.Map<PassengerType>(dto));

    public async Task<bool> UpdatePassengerTypeAsync(PassengerTypeDTO dto) =>  await repository.Update(mapper.Map<PassengerType>(dto));

    public async Task<bool> DeletePassengerTypeAsync(string id) =>  await repository.Delete(id);
}