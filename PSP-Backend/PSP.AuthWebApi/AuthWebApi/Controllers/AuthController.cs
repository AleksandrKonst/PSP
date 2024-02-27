using System.Security.Claims;
using AuthWebApi.DTO.ViewModels.Auth;
using AuthWebApi.Models;
using AuthWebApi.Service;
using AutoMapper;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
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
    public IActionResult ForgotPassword(string returnUrl)
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
    {
        if (!ModelState.IsValid)
            return View(forgotPasswordViewModel);
        var user = await userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
        if (user == null)
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var callback = Url.Action(nameof(ChangePassword), "Auth", new { token, email = user.Email }, Request.Scheme);
        await EmailService.SendChangeEmailAsync(forgotPasswordViewModel.Email, callback);
        
        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    }
    
    [HttpGet]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult ChangePassword(string token, string email)
    {
        var model = new ChangePasswordViewModel { Token = token, Email = email };
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
    {
        if (!ModelState.IsValid)
            return View(changePasswordViewModel);
        var user = await userManager.FindByEmailAsync(changePasswordViewModel.Email);
        if (user == null)
            RedirectToAction(nameof(ChangePasswordConfirmation));
        
        var resetPassResult = await userManager.ResetPasswordAsync(user, changePasswordViewModel.Token, changePasswordViewModel.Password);
        
        if (resetPassResult.Succeeded) return RedirectToAction(nameof(ChangePasswordConfirmation));
        
        foreach (var error in resetPassResult.Errors)
        {
            ModelState.TryAddModelError(error.Code, error.Description);
        }
        return View();
    }
    
    [HttpGet]
    public IActionResult ChangePasswordConfirmation()
    {
        return View();
    }
    
    public IActionResult ExternalLogin(string provider, string returnUrl)
    {
        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl });
        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        
        return Challenge(properties, provider);
    }
    
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
    {
        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return RedirectToAction("Login");
        }
    
        var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, false);
        if (result.Succeeded)
        {
            return RedirectToAction("Index","Manage");
        }

        var user = new PspUser()
        {
            UserName = info.Principal.FindFirstValue(ClaimTypes.Name),
            Name = "Яндекс",
            Surname = "Яндекс"
        };
        
        var resultReg = await userManager.CreateAsync(user);

        if (!resultReg.Succeeded) return BadRequest();
        
        var userFromDb = await userManager.FindByNameAsync(user.UserName);
        await userManager.AddToRoleAsync(userFromDb, "Passenger");
        var identityResult = await userManager.AddLoginAsync(user, info);

        if (!identityResult.Succeeded) return BadRequest();
        
        await signInManager.SignInAsync(user, false);
        return RedirectToAction("Index","Manage");
    }
}