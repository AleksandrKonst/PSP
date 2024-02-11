using AuthWebApi.DTO.ViewModels.Auth;
using AuthWebApi.DTO.ViewModels.Client;
using AuthWebApi.Models;
using AutoMapper;
using IdentityServer4.Models;

namespace AuthWebApi.DTO.AutoMapperProfiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<RegisterViewModel, PspUser>().ReverseMap();
        CreateMap<ViewModels.Manage.EditViewModel, PspUser>().ReverseMap();
        CreateMap<ViewModels.Manage.DeleteViewModel, PspUser>().ReverseMap();
        CreateMap<ViewModels.Manage.CreateViewModel, PspUser>().ReverseMap();
        CreateMap<UserDTO, PspUser>().ReverseMap();
        CreateMap<ClientEntity, Client>().ReverseMap();
        CreateMap<InfoViewModel, Client>().ReverseMap();
        CreateMap<ViewModels.Client.DeleteViewModel, Client>().ReverseMap();
    }
}