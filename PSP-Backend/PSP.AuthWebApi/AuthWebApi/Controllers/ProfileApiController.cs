using System.Dynamic;
using AuthWebApi.DTO;
using AuthWebApi.Exceptions;
using AuthWebApi.Filters;
using AuthWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApi.Controllers;

[Authorize]
[Controller]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("profile/api")]
public class ProfileApiController(RoleManager<IdentityRole> roleManager, UserManager<PspUser> userManager, IMapper mapper) : ControllerBase
{
    [HttpGet("{id}")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            throw new ResponseException("Пользователь не найден", "PPC-000403");
        }

        var userDto = mapper.Map<UserDTO>(user);
        userDto.Role = (await userManager.GetRolesAsync(user)).First();
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.passenger = userDto;
        
        return Ok(response);
    }
    
    [HttpPut]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] EditUserDTO userDto, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var user = await userManager.FindByIdAsync(userDto.Id);

        if (user == null)
        {
            throw new ResponseException("Пользователь не найден", "PPC-000403");
        }

        user.Name = userDto.Name;
        user.Surname = userDto.Surname;
        user.Patronymic = userDto.Patronymic;
        user.Birthday = userDto.Birthday;
        user.Email = userDto.Email;
        user.EmailConfirmed = userDto.EmailConfirmed;
        
        var result = await userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            var roleResult = await userManager.RemoveFromRoleAsync(user, (await userManager.GetRolesAsync(user)).First());

            if (roleResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, userDto.Role);
                response.service_data = new
                {
                    request_id = Guid.NewGuid().ToString(),
                    request_datetime = requestDateTime,
                    response_datetime = DateTime.Now,
                    mesaage = "Пользователь изменен"
                };
                return Ok(response); 
            }
        }
        
        throw new ResponseException("Ошибка изменения пользователя", "PPC-000500");
    }
}