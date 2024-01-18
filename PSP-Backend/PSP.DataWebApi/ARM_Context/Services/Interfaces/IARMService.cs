using PSP.DataWebApi.ARM_Context.DTO;

namespace PSP.DataWebApi.ARM_Context.Services.Interfaces;

public interface IARMService
{
    Task<List<dynamic>> SelectAsync(IEnumerable<SelectPassengerRequestDTO> selectPassengerRequestsDto);
}