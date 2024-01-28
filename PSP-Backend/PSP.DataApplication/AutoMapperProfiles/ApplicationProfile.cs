using AutoMapper;
using PSP.DataApplication.DTO;
using PSP.Domain.Models;
using PSP.DataApplication.DTO.ArmContextDTO.Search;
using PSP.DataApplication.DTO.ArmContextDTO.Select;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.DataApplication.DTO.PassengerContextDTO;
using PSP.DataApplication.Infrastructure;

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
        
        CreateMap<SearchRequestDTO, SearchByPassengerDTO>().ReverseMap();
        CreateMap<SearchRequestDTO, SearchByTicketDTO>().ReverseMap();
        CreateMap<SearchByPassengerDTO, SelectPassengerDataDTO>()
            .ForMember(p => p.DocumentNumbersLatin, 
                opt => opt.MapFrom(src => new List<string>() {ConvertStringService.Transliterate(src.DocumentNumber)})).ReverseMap();
        CreateMap<SelectPassengerRequestDTO, SelectPassengerDataDTO>()
            .ForMember(p => p.DocumentNumbersLatin, 
                opt => opt.MapFrom(src => new List<string>() {ConvertStringService.Transliterate(src.DocumentNumber)})).ReverseMap();
        CreateMap<SelectPassengerRequestDTO, PassengerDTO>().ReverseMap();
        CreateMap<FareDTO, InsertFaresDTO>().ReverseMap();
        CreateMap<InsertCouponDTO, FlightDTO>()
            .ForMember(p => p.Code,
                opt => opt.MapFrom(src => src.FlightNumber))
            .ForMember(p => p.DepartDateTimePlan,
                opt => opt.MapFrom(src => DateTime.Parse(src.DepartTimePlan).ToUniversalTime()))
            .ForMember(p => p.ArriveDateTimePlan,
                opt => opt.MapFrom(src => DateTime.Parse(src.ArriveTimePlan).ToUniversalTime()))
            .ForMember(p => p.FareCode,
            opt => opt.MapFrom(src => "")).ReverseMap();
    }
}