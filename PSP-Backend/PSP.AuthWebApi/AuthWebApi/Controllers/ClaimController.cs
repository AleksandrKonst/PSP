using System.Text;
using AuthWebApi.DTO;
using AuthWebApi.DTO.ViewModels.Claim;
using AuthWebApi.Models.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthWebApi.Controllers;

[Authorize(Roles = "Admin")]
[Controller]
public class ClaimController(AuthDbContext context, IMapper mapper, ILogger<AuthController> logger) : Controller
{
    private const int PageSize = 10;

    [HttpGet]
    public async Task<IActionResult> Index(string? search, int page = 1)
    {
        ViewBag.SelectedCategory = "users";
        
        var claims = await context.UserClaims
            .Where(r => search == null || r.ClaimType!.ToLower().Contains(search.ToLower()) 
                                       || r.ClaimValue!.ToLower().Contains(search.ToLower()))
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .AsNoTracking()
            .ToListAsync();
        
        var indexViewModel = new IndexViewModel()
        {
            Search = search ?? "",
            MaxPage = claims.Count / PageSize + 1,
            Page = page,
            Claims = mapper.Map<IEnumerable<ClaimDTO>>(claims)
        };
        
        return View(indexViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ExportCsv()
    {
        var claims = await context.UserClaims.Distinct().ToListAsync();

        var builder = new StringBuilder();
        builder.AppendLine("UserId,ClaimType,ClaimValue");

        foreach (var claim in claims)
        {
            builder.AppendLine($"{claim.UserId},{claim.ClaimType},{claim.ClaimValue}");
        }
        
        return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "claims.csv");
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
        
        logger.LogInformation($"Edit: {claim.ClaimType} : {claim.ClaimValue}");
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
        
        logger.LogInformation($"Delete: {claim.ClaimType} : {claim.ClaimValue}");
        return RedirectToAction(nameof(Index));
    }
}