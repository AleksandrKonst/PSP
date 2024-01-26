using AutoMapper;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.ArmContextDTO.Select;

namespace PSP.DataWebApi.Contexts.ARM_Context.DTO.AutoMapperProfiles;

public class ARMProfile : Profile
{
    public ARMProfile()
    {
        CreateMap<PostSelectPassengerRequestDTO, SelectPassengerRequestDTO>().ReverseMap();
    }
}