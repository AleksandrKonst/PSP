using AuthWebApi.DTO;
using AuthWebApi.DTO.ViewModels.Auth;
using AuthWebApi.DTO.ViewModels.Manage;
using AuthWebApi.Models;
using AutoMapper;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthWebApi.Controllers;

[Authorize]
public class ManageController(SignInManager<PspUser> signInManager, UserManager<PspUser> userManager,
        IIdentityServerInteractionService interactionService, RoleManager<IdentityRole> roleManager, IMapper mapper)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewBag.SelectedCategory = "users";
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
    public IActionResult Create(string returnUrl)
    {
        ViewBag.SelectedCategory = "users";
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

        var user = mapper.Map<PspUser>(viewModel);
        
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

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        ViewBag.SelectedCategory = "users";
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return NoContent();
        }
        
        return View(mapper.Map<EditViewModel>(user));
    }

    [HttpGet]
    public async Task<IActionResult> Delete()
    {
        ViewBag.SelectedCategory = "users";
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return NoContent();
        }
        
        return View(mapper.Map<DeleteViewModel>(user));
    }
}