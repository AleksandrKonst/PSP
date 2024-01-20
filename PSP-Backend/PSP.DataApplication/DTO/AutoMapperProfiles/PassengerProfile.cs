using AutoMapper;
using PSP.Domain.Models;

namespace PSP.DataApplication.DTO.AutoMapperProfiles;

public class PassengerProfile : Profile
{
    public PassengerProfile()
    {
        CreateMap<PassengerDTO, Passenger>().ReverseMap();
        CreateMap<PassengerTypeDTO, PassengerType>().ReverseMap();
        CreateMap<DocumentTypeDTO, DocumentType>().ReverseMap();
        CreateMap<PassengerQuotaCountDTO, PassengerQuotaCount>().ReverseMap();
        
        CreateMap<SelectPassengerResponseDTO, Passenger>()
            .ForMember(response => response.DocumentTypeCode, 
                opt => opt.MapFrom(
                    request => request.DocumentType))
            .ReverseMap();
    }
}