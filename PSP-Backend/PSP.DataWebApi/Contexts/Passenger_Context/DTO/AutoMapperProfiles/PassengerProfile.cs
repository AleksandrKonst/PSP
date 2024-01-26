using AutoMapper;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.PassengerContextDTO;

namespace PSP.DataWebApi.Contexts.Passenger_Context.DTO.AutoMapperProfiles;

public class PassengerProfile : Profile
{
    public PassengerProfile()
    {
        CreateMap<PostDocumentTypeDTO, DocumentTypeDTO>().ReverseMap();
        CreateMap<PostPassengerDTO, PassengerDTO>().ReverseMap();
    }
}