using AutoMapper;
using PSP.Domain.Models;
using PSP.RouteApplication.DTO;

namespace PSP.RouteApplication.AutoMapperProfiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Flight, FlightViewModel>()
            .ForMember(p => p.Airline, 
            opt => opt.MapFrom(src => src.AirlineCodeNavigation))
            .ForMember(p => p.FlightSegments, 
                opt => opt.MapFrom(src => src.FlightSegments))
            .ForMember(p => p.Fare, 
                opt => opt.MapFrom(src => src.FareCodeNavigation))
            .ForMember(p => p.ArrivePlaceModel, 
                opt => opt.MapFrom(src => src.ArrivePlaceNavigation))
            .ForMember(p => p.DepartPlaceModel, 
                opt => opt.MapFrom(src => src.DepartPlaceNavigation))
            .ReverseMap();
        CreateMap<Fare, FareViewModel>().ReverseMap();
        CreateMap<Airline, AirlineViewModel>().ReverseMap();
        CreateMap<Airport, AirportViewModel>().ReverseMap();
        CreateMap<FlightSegment, FlightSegmentViewModel>().ReverseMap();
    }
}