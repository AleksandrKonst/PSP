using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace AuthWebApi.Models.Infrastructure;

public class UserProfileService(UserManager<PspUser> userManager, RoleManager<IdentityRole> roleManager) : IProfileService
{

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);
            
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var roles = await userManager.GetRolesAsync(user);

        var myClaims = new List<Claim>()
        {
            new("login", user.UserName),
            new("name", user.Name),
            new("surname", user.Surname),
            new("patronymic", user.Patronymic ?? " "),
            new("email", user.Email),
            new("birthday", user.Birthday.Value.ToString("yyyy-MM-dd")),
            new("roles", roles.First())
        };
        
        myClaims.AddRange(await userManager.GetClaimsAsync(user));

        context.IssuedClaims = myClaims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
    }
}