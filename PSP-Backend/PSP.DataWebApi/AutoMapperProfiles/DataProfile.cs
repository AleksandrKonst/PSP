using AutoMapper;
using PSP.DataApplication.DTO.ArmContextDTO.Search;

namespace PSP.DataWebApi.AutoMapperProfiles;

public class DataProfile : Profile
{
    public DataProfile()
    {
        CreateMap<SearchRequestDTO, SearchByPassengerDTO>().ReverseMap();
        CreateMap<SearchRequestDTO, SearchByTicketDTO>().ReverseMap();
    }
}