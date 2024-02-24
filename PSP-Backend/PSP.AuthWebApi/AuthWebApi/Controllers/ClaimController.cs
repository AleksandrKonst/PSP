using AuthWebApi.DTO;
using AuthWebApi.DTO.ViewModels.Claim;
using AuthWebApi.Models.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthWebApi.Controllers;

[Authorize]
[Controller]
public class ClaimController(AuthDbContext context, IMapper mapper) : Controller
{
    private const int PageSize = 8;

    [HttpGet]
    public async Task<IActionResult> Index(string search, int page = 1)
    {
        ViewBag.SelectedCategory = "users";
        
        var claims = await context.UserClaims
            .Where(r => search == null || r.ClaimType.ToLower().Contains(search.ToLower()) 
                                       || r.ClaimValue.ToLower().Contains(search.ToLower()))
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
        
        var indexViewModel = new IndexViewModel()
        {
            Search = search,
            MaxPage = claims.Count / PageSize + 1,
            Page = page,
            Claims = mapper.Map<IEnumerable<ClaimDTO>>(claims)
        };
        
        return View(indexViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.SelectedCategory = "users";
        
        var role = await context.UserClaims.FindAsync(id);

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

        var claim = await context.UserClaims.FindAsync(viewModel.Id);

        if (claim == null)
        {
            ModelState.AddModelError(string.Empty, "Claim not found");
            return View(viewModel);
        }

        claim.UserId = viewModel.UserId;
        claim.ClaimType = viewModel.ClaimType;
        claim.ClaimValue = viewModel.ClaimValue;

        context.UserClaims.Update(claim);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Edit), new {claim.Id});
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        ViewBag.SelectedCategory = "users";

        var claim = await context.UserClaims.FindAsync(id);
        
        if (claim == null)
        {
            return NoContent();
        }
        return View(mapper.Map<DeleteViewModel>(claim));
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        ViewBag.SelectedCategory = "users";
        var claim = await context.UserClaims.FindAsync(id);

        if (claim == null)
        {
            return NoContent();
        }

        context.UserClaims.Remove(claim);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}