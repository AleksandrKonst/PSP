using Application.DTO.ArmContextDTO.Search;
using AutoMapper;

namespace WebApi.AutoMapperProfiles;

public class DataProfile : Profile
{
    public DataProfile()
    {
        CreateMap<SearchRequestDTO, SearchByPassengerDTO>().ReverseMap();
        CreateMap<SearchRequestDTO, SearchByTicketDTO>().ReverseMap();
    }
}