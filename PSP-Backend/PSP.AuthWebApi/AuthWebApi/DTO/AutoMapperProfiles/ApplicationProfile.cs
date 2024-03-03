using System.Security.Claims;
using AuthWebApi.DTO.ViewModels.Auth;
using AuthWebApi.Models;
using AutoMapper;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;

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
        CreateMap<EditUserDTO, PspUser>().ReverseMap();
        CreateMap<ClientEntity, Client>().ReverseMap();
        CreateMap<ViewModels.Client.InfoViewModel, Client>().ReverseMap();
        CreateMap<ViewModels.Client.DeleteViewModel, Client>().ReverseMap();
        CreateMap<ViewModels.Role.DeleteViewModel, IdentityRole>().ReverseMap();
        CreateMap<ViewModels.Role.EditViewModel, IdentityRole>().ReverseMap();
        CreateMap<RoleDTO, IdentityRole>().ReverseMap();
        CreateMap<ClaimDTO, Claim>().ReverseMap();
        CreateMap<ClaimDTO, IdentityUserClaim<string>>().ReverseMap();
        CreateMap<ViewModels.Claim.EditViewModel, IdentityUserClaim<string>>().ReverseMap();
        CreateMap<ViewModels.Claim.DeleteViewModel, IdentityUserClaim<string>>().ReverseMap();
    }
}