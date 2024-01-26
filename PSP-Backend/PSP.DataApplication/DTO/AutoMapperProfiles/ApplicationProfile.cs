using AutoMapper;
using PSP.DataApplication.DTO.ArmContextDTO.Select;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.DataApplication.DTO.PassengerContextDTO;
using PSP.Domain.Models;

namespace PSP.DataApplication.DTO.AutoMapperProfiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<PassengerDTO, Passenger>().ReverseMap();
        CreateMap<PassengerTypeDTO, PassengerType>().ReverseMap();
        CreateMap<DocumentTypeDTO, DocumentType>().ReverseMap();
        CreateMap<PassengerQuotaCountDTO, PassengerQuotaCount>().ReverseMap();

        CreateMap<CouponEventDTO, CouponEvent>().ReverseMap();
        
        CreateMap<SelectPassengerDataDTO, Passenger>()
            .ForMember(response => response.DocumentTypeCode, 
                opt => opt.MapFrom(
                    request => request.DocumentType))
            .ReverseMap();
    }
}