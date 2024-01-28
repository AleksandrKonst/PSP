using AutoMapper;
using PSP.Domain.Models;
using PSP.DataApplication.DTO.ArmContextDTO.Search;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.DataApplication.DTO.PassengerContextDTO;

namespace PSP.DataApplication.AutoMapperProfiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<PassengerDTO, Passenger>().ReverseMap();
        CreateMap<PassengerTypeDTO, PassengerType>().ReverseMap();
        CreateMap<DocumentTypeDTO, DocumentType>().ReverseMap();

        CreateMap<CouponEventDTO, CouponEvent>().ReverseMap();
        CreateMap<FareDTO, Fare>().ReverseMap();
        CreateMap<FlightDTO, Flight>().ReverseMap();
        CreateMap<SearchRequestDTO, SearchPassengerDTO>().ReverseMap();
        CreateMap<SearchRequestDTO, SearchTicketDTO>().ReverseMap();
    }
}