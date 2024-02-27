using AuthWebApi.DTO.ViewModels.Auth;
using AuthWebApi.Models;
using AuthWebApi.Service;
using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApi.Controllers;

public class AuthController(SignInManager<PspUser> signInManager, UserManager<PspUser> userManager,
        IIdentityServerInteractionService interactionService, RoleManager<IdentityRole> roleManager, IMapper mapper) : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel(){ReturnUrl = returnUrl});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return Redirect(viewModel.ReturnUrl);
        }

        var user = await userManager.FindByNameAsync(viewModel.Username);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Login Error");
            return Redirect(viewModel.ReturnUrl);
        }

        var result = await signInManager.PasswordSignInAsync(user,
            viewModel.Password, false, false);
        if (result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Password Error");
            return Redirect(viewModel.ReturnUrl);
        }
        
        ModelState.AddModelError(string.Empty, "Login Form Error");
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        var user = mapper.Map<PspUser>(viewModel);
        
        var result = await userManager.CreateAsync(user, viewModel.Password);
        if (result.Succeeded)
        {
            var userFromDb = await userManager.FindByNameAsync(user.UserName);
            await userManager.AddToRoleAsync(userFromDb, "Passenger");
            
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { token, email = user.Email }, Request.Scheme);
            await EmailService.SendEmailAsync(viewModel.Email, confirmationLink);
            
            return RedirectToAction(nameof(SuccessRegistration));
        }
        ModelState.AddModelError(string.Empty, "Error occurred");
        return View(viewModel);
    }
    
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return BadRequest();
        var result = await userManager.ConfirmEmailAsync(user, token);
        return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
    }
    
    [HttpGet]
    public IActionResult SuccessRegistration()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }
    
    [HttpGet]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
    {
        return View(forgotPasswordViewModel);
    }
}