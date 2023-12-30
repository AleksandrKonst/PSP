using System.Dynamic;
using AutoMapper;
using PSP_Data_Service.Passenger_Context.DTO;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;
using PSP_Data_Service.Passenger_Context.Services.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Services;

public class PassengerService(IPassengerRepository repository, IMapper mapper) : IPassengerService
{
    public async Task<dynamic> GetPassengersAsync(DateTime requestDateTime)
    {
        dynamic passengerInfo = new ExpandoObject();
        passengerInfo.passengers = mapper.Map<IEnumerable<PassengerDTO>>(await repository.GetAllAsync());
        passengerInfo.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now
        };
        return passengerInfo;
    }
}