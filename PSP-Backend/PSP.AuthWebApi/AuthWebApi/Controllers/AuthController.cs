using AuthWebApi.DTO.ViewModels.Auth;
using AuthWebApi.Models;
using AutoMapper;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApi.Controllers;

public class AuthController(SignInManager<PspUser> signInManager, UserManager<PspUser> userManager,
        IIdentityServerInteractionService interactionService, RoleManager<IdentityRole> roleManager, IMapper mapper)
    : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel(){ReturnUrl = returnUrl});
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return Redirect(viewModel.ReturnUrl);
        }

        var user = await userManager.FindByNameAsync(viewModel.Username);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Login or Password Error");
            return Redirect(viewModel.ReturnUrl);
        }

        var result = await signInManager.PasswordSignInAsync(user,
            viewModel.Password, false, false);
        if (result.Succeeded)
        {
            return Redirect(viewModel.ReturnUrl);
        }
        
        ModelState.AddModelError(string.Empty, "Login error");
        return Redirect(viewModel.ReturnUrl);
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        await signInManager.SignOutAsync();

        var logoutRequest = await interactionService.GetLogoutContextAsync(logoutId);

        if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
        {
            return RedirectToAction();
        }

        return Redirect(logoutRequest.PostLogoutRedirectUri);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login), "Auth", new
        {
            ReturnUrl = "/manage/index"
        });
    }
    
    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
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
}