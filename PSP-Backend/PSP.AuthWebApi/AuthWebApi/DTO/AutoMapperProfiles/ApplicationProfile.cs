using AuthWebApi.DTO.ViewModels.Auth;
using AuthWebApi.DTO.ViewModels.Manage;
using AuthWebApi.Models;
using AutoMapper;

namespace AuthWebApi.DTO.AutoMapperProfiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<RegisterViewModel, PspUser>().ReverseMap();
        CreateMap<EditViewModel, PspUser>().ReverseMap();
        CreateMap<DeleteViewModel, PspUser>().ReverseMap();
        CreateMap<CreateViewModel, PspUser>().ReverseMap();
    }
}