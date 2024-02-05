using AuthWebApi.DTO;
using AuthWebApi.DTO.ViewModels.Auth;
using AuthWebApi.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthWebApi.Controllers;

[Authorize]
public class ManageController(SignInManager<PspUser> signInManager, UserManager<PspUser> userManager,
        IIdentityServerInteractionService interactionService, RoleManager<IdentityRole> roleManager)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }
        
        Console.WriteLine(user.UserName);

        //var roles = await roleManager.Roles.Select(r => r.Name).ToListAsync();
        var users = await userManager.Users.Select(u => new UserDTO
        {
            Id = u.Id,
            Name = u.UserName,
            Surname = u.Surname,
            Patronymic = u.Patronymic,
            Email = u.Email,
            EmailConfirmed = u.EmailConfirmed
        }).ToListAsync();

        return View(users);
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Create(string returnUrl)
    {
        return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Create(RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        if (!await roleManager.RoleExistsAsync("Passenger"))
        {
            await roleManager.CreateAsync(new IdentityRole("Passenger"));
        }

        var user = new PspUser()
        {
            UserName = viewModel.Username,
            Surname = "viewModel"
        };
        
        var result = await userManager.CreateAsync(user, viewModel.Password);
        if (result.Succeeded)
        {
            var userFromDb = await userManager.FindByNameAsync(user.UserName);
            
            await signInManager.SignInAsync(user, false);
            await userManager.AddToRoleAsync(userFromDb, "Passenger");
            return Redirect(viewModel.ReturnUrl);
        }
        ModelState.AddModelError(string.Empty, "Error occurred");
        return View(viewModel);
    }

    public IActionResult Edit()
    {
        throw new NotImplementedException();
    }

    public IActionResult Delete()
    {
        throw new NotImplementedException();
    }
}