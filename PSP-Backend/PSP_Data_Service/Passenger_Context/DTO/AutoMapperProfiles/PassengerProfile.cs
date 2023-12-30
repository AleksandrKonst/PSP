using AutoMapper;
using PSP_Data_Service.Passenger_Context.Models;

namespace PSP_Data_Service.Passenger_Context.DTO.AutoMapperProfiles;

public class PassengerProfile : Profile
{
    public PassengerProfile()
    {
        CreateMap<PassengerDTO, Passenger>().ReverseMap();
    }
}