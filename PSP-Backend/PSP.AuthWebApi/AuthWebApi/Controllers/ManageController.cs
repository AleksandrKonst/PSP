using System.Security.Claims;
using AuthWebApi.DTO;
using AuthWebApi.DTO.ViewModels.Manage;
using AuthWebApi.Models;
using AuthWebApi.Models.Data;
using AutoMapper;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthWebApi.Controllers;

[Authorize]
[Controller]
public class ManageController(SignInManager<PspUser> signInManager, UserManager<PspUser> userManager,
        IIdentityServerInteractionService interactionService, RoleManager<IdentityRole> roleManager, IMapper mapper, AuthDbContext context)
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

        var users = await (from us in context.Users
            join userRole in context.UserRoles on us.Id equals userRole.UserId
            join role in context.Roles on userRole.RoleId equals role.Id
            select new UserDTO
            {
                Id = us.Id,
                Login = us.UserName,
                FIO = $"{us.Surname} {us.Name} {us.Patronymic}",
                Email = us.Email,
                EmailConfirmed = us.EmailConfirmed,
                Role = role.Name
            }).ToListAsync();

        return View(users);
    }
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.SelectedCategory = "users";
        ViewBag.Roles = await roleManager.Roles.ToListAsync();
        return View();
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CreateViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        ViewBag.SelectedCategory = "users";

        var user = mapper.Map<PspUser>(viewModel);
        
        var result = await userManager.CreateAsync(user, viewModel.Password);
        if (result.Succeeded)
        {
            var userFromDb = await userManager.FindByNameAsync(user.UserName);
            await userManager.AddToRoleAsync(userFromDb, viewModel.Role);
            return RedirectToAction(nameof(Create));
        }
        
        ModelState.AddModelError(string.Empty, "Error occurred");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        ViewBag.SelectedCategory = "users";
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NoContent();
        }

        var editUser = mapper.Map<EditViewModel>(user);
        editUser.Role = (await userManager.GetRolesAsync(user)).First();
        
        return View(editUser);
    }
    
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(EditViewModel viewModel)
    {
        ViewBag.SelectedCategory = "users";
        
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Model Error");
            return View(viewModel);
        }

        var user = await userManager.FindByNameAsync(viewModel.NormalizedUserName);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(viewModel);
        }

        user.Name = viewModel.Name;
        user.Surname = viewModel.Surname;
        user.Patronymic = viewModel.Patronymic;
        user.Birthday = viewModel.Birthday;
        
        var result = await userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return View(viewModel);
        }
        ModelState.AddModelError(string.Empty, "Error occurred");
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        ViewBag.SelectedCategory = "users";
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NoContent();
        }
        
        return View(mapper.Map<DeleteViewModel>(user));
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        ViewBag.SelectedCategory = "users";
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NoContent();
        }

        var result = await userManager.DeleteAsync(user);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> ChangePassword(string normalizedUserName)
    {
        ViewBag.SelectedCategory = "users";

        var user = new ChangePasswordViewModel()
        {
            NormalizedUserName = normalizedUserName
        };
        
        return View(user);
    }
    
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
    {
        ViewBag.SelectedCategory = "users";
        
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var user = await userManager.FindByNameAsync(viewModel.NormalizedUserName);
        if (user == null)
        {
            throw new ApplicationException($"Unable to load user with NormalizedUserName: {viewModel.NormalizedUserName}.");
        }

        var addPasswordResult = await userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.Password);
        if (!addPasswordResult.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Password: addPasswordResult");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Edit), new { id = user.Id });
    }
}