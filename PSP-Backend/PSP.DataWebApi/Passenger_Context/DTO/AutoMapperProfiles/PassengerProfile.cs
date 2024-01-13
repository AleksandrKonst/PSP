using AutoMapper;
using PSP.Domain.Models;

namespace PSP.DataWebApi.Passenger_Context.DTO.AutoMapperProfiles;

public class PassengerProfile : Profile
{
    public PassengerProfile()
    {
        CreateMap<PassengerDTO, Passenger>().ReverseMap();
        CreateMap<PassengerTypeDTO, PassengerType>().ReverseMap();
        CreateMap<DocumentTypeDTO, DocumentType>().ReverseMap();
        CreateMap<PassengerQuotaCountDTO, PassengerQuotaCount>().ReverseMap();
    }
}