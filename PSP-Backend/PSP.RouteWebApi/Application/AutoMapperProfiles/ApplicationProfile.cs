using Application.DTO;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapperProfiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Flight, FlightViewModel>()
            .ForMember(p => p.Fare, 
                opt => opt.MapFrom(src => src.FareCodeNavigation))
            .ForMember(p => p.ArrivePlaceModel, 
                opt => opt.MapFrom(src => src.ArrivePlaceNavigation))
            .ForMember(p => p.DepartPlaceModel, 
                opt => opt.MapFrom(src => src.DepartPlaceNavigation))
            .ReverseMap();
        CreateMap<Flight, FlightSegmentViewModel>()
            .ForMember(p => p.Airline, 
            opt => opt.MapFrom(src => src.AirlineCodeNavigation)).ReverseMap();
        CreateMap<Fare, FareViewModel>().ReverseMap();
        CreateMap<Airline, AirlineViewModel>().ReverseMap();
        CreateMap<Airport, AirportViewModel>().ReverseMap();
    }
}