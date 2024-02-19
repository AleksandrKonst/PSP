using Application.DTO.ArmContextDTO.Insert;
using Application.DTO.ArmContextDTO.Search;
using Application.DTO.ArmContextDTO.Select;
using Application.DTO.FlightContextDTO;
using Application.DTO.PassengerContextDTO;
using Application.Infrastructure;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapperProfiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<PassengerDTO, Passenger>().ReverseMap();
        CreateMap<PassengerTypeDTO, PassengerType>().ReverseMap();
        CreateMap<DocumentTypeDTO, DocumentType>().ReverseMap();
        CreateMap<GenderTypeDTO, GenderType>().ReverseMap();
        
        CreateMap<AirlineDTO, Airline>().ReverseMap();
        CreateMap<AirportDTO, Airport>().ReverseMap();
        CreateMap<CityDTO, City>().ReverseMap();
        CreateMap<CouponEventDTO, CouponEvent>().ReverseMap();
        CreateMap<FareDTO, Fare>().ReverseMap();
        CreateMap<FlightDTO, Flight>().ReverseMap();
        CreateMap<SubsidizedRouteDTO, SubsidizedRoute>().ReverseMap();
        CreateMap<QuotaCategoryDTO, QuotaCategory>().ReverseMap();
        CreateMap<TicketTypeDTO, TicketType>().ReverseMap();
        
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