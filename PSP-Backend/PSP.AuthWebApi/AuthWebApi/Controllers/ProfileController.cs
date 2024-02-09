using AuthWebApi.DTO.ViewModels.Manage;
using AuthWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApi.Controllers;

[Authorize]
public class ProfileController(UserManager<PspUser> userManager, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewBag.SelectedCategory = "profile";
        
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return NoContent();
        }
        
        return View(mapper.Map<DeleteViewModel>(user));
    }
}