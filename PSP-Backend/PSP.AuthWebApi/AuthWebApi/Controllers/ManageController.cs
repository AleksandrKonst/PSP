using System.Security.Claims;
using AuthWebApi.DTO;
using AuthWebApi.DTO.ViewModels.Manage;
using AuthWebApi.Models;
using AuthWebApi.Models.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthWebApi.Controllers;

[Controller]
[Authorize(Roles = "Admin, Airline")]
public class ManageController(UserManager<PspUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, AuthDbContext context) : Controller
{
    private const int PageSize = 10;

    [HttpGet]
    public async Task<IActionResult> Index(string search, int page = 1)
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
            where (search == null || us.UserName.ToLower().Contains(search.ToLower()) || us.Name.ToLower().Contains(search.ToLower()) 
                   || us.Surname.ToLower().Contains(search.ToLower()) || us.Patronymic.ToLower().Contains(search.ToLower()) 
                   || role.Name.ToLower().Contains(search.ToLower())) && us.UserName != user.UserName
            select new UserDTO
            {
                Id = us.Id,
                Login = us.UserName,
                FIO = $"{us.Surname} {us.Name} {us.Patronymic}",
                Email = us.Email,
                EmailConfirmed = us.EmailConfirmed,
                Role = role.Name
            }).Skip((page - 1) * PageSize).Take(PageSize)
            .ToListAsync();

        var indexViewModel = new IndexViewModel()
        {
            Search = search,
            MaxPage = users.Count / PageSize + 1,
            Page = page,
            Users = users
        };
        
        return View(indexViewModel);
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
        ViewBag.SelectedCategory = "users";
        
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Error occurred");
            return View();
        }

        var user = mapper.Map<PspUser>(viewModel);
        
        var result = await userManager.CreateAsync(user, viewModel.Password);
        if (result.Succeeded)
        {
            var userFromDb = await userManager.FindByNameAsync(user.UserName);
            await userManager.AddToRoleAsync(userFromDb, viewModel.Role);
            return RedirectToAction(nameof(Create));
        }

        ViewBag.Roles = await roleManager.Roles.ToListAsync();
        ModelState.AddModelError(string.Empty, "User error occurred");
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
        
        ViewBag.Roles = await roleManager.Roles.ToListAsync();
        
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
        user.Email = viewModel.Email;
        user.EmailConfirmed = viewModel.EmailConfirmed;
        
        var result = await userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            var roleResult = await userManager.RemoveFromRoleAsync(user, (await userManager.GetRolesAsync(user)).First());

            if (roleResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, viewModel.Role);
                return RedirectToAction(nameof(Edit), new {user.Id});
            }
        }
        ModelState.AddModelError(string.Empty, "Error occurred");
        return View(viewModel);
    }
    
    [HttpGet]
    public async Task<IActionResult> EditClaim(string id)
    {
        ViewBag.SelectedCategory = "users";
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NoContent();
        }
        
        var editUser = new EditClaimViewModel()
        {
            UserId = user.Id,
            Claims = await context.UserClaims
                .Where(u => u.UserId == user.Id)
                .Select(u => new ClaimDTO()
                {
                    Id = u.Id,
                    UserId = u.UserId,
                    ClaimType = u.ClaimType,
                    ClaimValue = u.ClaimValue
                }).ToListAsync()
        };
        
        return View(editUser);
    }
    
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> EditClaim(EditClaimViewModel viewModel)
    {
        ViewBag.SelectedCategory = "users";

        var user = await userManager.FindByIdAsync(viewModel.UserId);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(viewModel);
        }

        var result = await userManager.AddClaimAsync(user, new Claim(viewModel.ClaimType, viewModel.ClaimValue));
        
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(EditClaim), new {id = viewModel.UserId});
        }
        ModelState.AddModelError(string.Empty, "Error occurred");
        return View(viewModel);
    }
    
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> DeleteClaim(long id)
    {
        ViewBag.SelectedCategory = "users";
        
        var claim = await context.UserClaims.Where(u => u.Id == id).FirstAsync();
        var user = await userManager.FindByIdAsync(claim.UserId);
        await userManager.RemoveClaimAsync(user, claim.ToClaim());

        var editUser = new EditClaimViewModel()
        {
            UserId = user.Id,
            Claims = await context.UserClaims
                .Where(u => u.UserId == user.Id)
                .Select(u => new ClaimDTO()
                {
                    Id = u.Id,
                    UserId = u.UserId,
                    ClaimType = u.ClaimType,
                    ClaimValue = u.ClaimValue
                }).ToListAsync()
        };
        
        return View("EditClaim", editUser);
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
        
        ViewBag.Role = (await userManager.GetRolesAsync(user)).First();
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
    public async Task<IActionResult> ChangePassword(string id)
    {
        ViewBag.SelectedCategory = "users";
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NoContent();
        }

        var userChange = new ChangePasswordViewModel()
        {
            Id = id,
            NormalizedUserName = user.NormalizedUserName
        };
        
        return View(userChange);
    }
    
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
    {
        ViewBag.SelectedCategory = "users";
        
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Model Error");
            return View(viewModel);
        }

        var user = await userManager.FindByNameAsync(viewModel.Id);
        if (user == null)
        {
            throw new ApplicationException($"Unable to load user with NormalizedUserName: {viewModel.NormalizedUserName}.");
        }

        var addPasswordResult = await userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.Password);
        if (!addPasswordResult.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Change Password False");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Edit), new { id = user.Id });
    }
}