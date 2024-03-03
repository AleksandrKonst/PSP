using AuthWebApi.DTO;
using AuthWebApi.DTO.ViewModels.Role;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthWebApi.Controllers;

[Authorize]
[Controller]
public class RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper) : Controller
{
    private const int PageSize = 10;

    [HttpGet]
    public async Task<IActionResult> Index(string search, int page = 1)
    {
        ViewBag.SelectedCategory = "users";
        
        var roles = await roleManager.Roles
            .Where(r => search == null || r.Name.ToLower().Contains(search.ToLower()))
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
        
        var indexViewModel = new IndexViewModel()
        {
            Search = search,
            MaxPage = roles.Count / PageSize + 1,
            Page = page,
            Roles = mapper.Map<IEnumerable<RoleDTO>>(roles)
        };
        
        return View(indexViewModel);
    }
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.SelectedCategory = "users";
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

        if (await roleManager.RoleExistsAsync(viewModel.Name))
        {
            ModelState.AddModelError(string.Empty, "Role Exist");
            return View();
        }
        await roleManager.CreateAsync(new IdentityRole(viewModel.Name));
        return RedirectToAction(nameof(Create));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        ViewBag.SelectedCategory = "users";
        
        var role = await roleManager.FindByIdAsync(id);

        if (role == null)
        {
            return NoContent();
        }
        
        var editRole = mapper.Map<EditViewModel>(role);

        return View(editRole);
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

        var role = await roleManager.FindByNameAsync(viewModel.NormalizedName);

        if (role == null)
        {
            ModelState.AddModelError(string.Empty, "Role not found");
            return View(viewModel);
        }

        role.Name = viewModel.Name;
        role.NormalizedName = viewModel.NormalizedName;
        
        var result = await roleManager.UpdateAsync(role);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Edit), new {role.Id});
        }
        ModelState.AddModelError(string.Empty, "Error occurred");
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        ViewBag.SelectedCategory = "users";

        var role = await roleManager.FindByIdAsync(id);
        
        if (role == null)
        {
            return NoContent();
        }
        return View(mapper.Map<DeleteViewModel>(role));
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        ViewBag.SelectedCategory = "users";
        var role = await roleManager.FindByIdAsync(id);

        if (role == null)
        {
            return NoContent();
        }

        var result = await roleManager.DeleteAsync(role);
        return RedirectToAction(nameof(Index));
    }
}