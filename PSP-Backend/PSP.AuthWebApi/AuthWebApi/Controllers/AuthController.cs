using System.Security.Claims;
using AuthWebApi.DTO.ViewModels.Auth;
using AuthWebApi.Models;
using AuthWebApi.Service;
using AutoMapper;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApi.Controllers;

[AllowAnonymous]
[Controller]
public class AuthController(SignInManager<PspUser> signInManager, UserManager<PspUser> userManager,
        IIdentityServerInteractionService interactionService, IMapper mapper, ILogger<AuthController> logger) : Controller
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
            ModelState.AddModelError(string.Empty, "Неверный логин");
            return View(new LoginViewModel(){ReturnUrl = viewModel.ReturnUrl});
        }

        var result = await signInManager.PasswordSignInAsync(user,
            viewModel.Password, false, false);
        if (result.Succeeded)
        {
            logger.LogInformation($"Login: {user.UserName}. Email {user.Email}");
            return Redirect(viewModel.ReturnUrl);
        }
        
        ModelState.AddModelError(string.Empty, "Неверный пароль");
        return View(new LoginViewModel(){ReturnUrl = viewModel.ReturnUrl});
    }
    
    [HttpGet]
    [Authorize]
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
    [Authorize]
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
            var userFromDb = await userManager.FindByNameAsync(user.UserName!);
            if (userFromDb == null)
                return BadRequest();
            await userManager.AddToRoleAsync(userFromDb, "Passenger");
            
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { token, email = user.Email }, Request.Scheme);
            if (confirmationLink == null)
                return BadRequest();
            await EmailService.SendEmailAsync(viewModel.Email, confirmationLink);
            
            logger.LogInformation($"Register: {user.UserName}. Email {user.Email}");
            return RedirectToAction(nameof(SuccessRegistration));
        }
        ModelState.AddModelError(string.Empty, "Error occurred");
        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult SuccessRegistration()
    {
        return View();
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
            return RedirectToAction(nameof(Login));
        
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var callback = Url.Action(nameof(ChangePassword), "Auth", new { token, email = user.Email }, Request.Scheme);
        
        if (callback == null)
            return RedirectToAction(nameof(Login));
        await EmailService.SendChangeEmailAsync(forgotPasswordViewModel.Email, callback);
        
        logger.LogInformation($"ForgotPassword Confirm: {user.UserName}. Email {user.Email}");
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
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Model error");
            return View(changePasswordViewModel);
        }
        
        var user = await userManager.FindByEmailAsync(changePasswordViewModel.Email);
        if (user == null)
            return RedirectToAction(nameof(Login));
        
        var resetPassResult = await userManager.ResetPasswordAsync(user, changePasswordViewModel.Token, changePasswordViewModel.Password);
        
        if (resetPassResult.Succeeded)
        {
            logger.LogInformation($"ChangePassword: {user.UserName}. Email {user.Email}");
            return RedirectToAction(nameof(ChangePasswordConfirmation));
        }
        
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
    
    [HttpPost]
    public IActionResult ExternalLogin(string provider, string returnUrl)
    {
        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl });
        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        
        return Challenge(properties, provider);
    }
    
    [HttpGet]
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
            return Redirect(returnUrl);
        }
        
        var user = new PspUser()
        {
            UserName = $"{info.LoginProvider}-{info.Principal.FindFirstValue( ClaimTypes.NameIdentifier)}",
            Name = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? "External404",
            Surname = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? "External404",
            Birthday = DateOnly.Parse(info.Principal.FindFirstValue(ClaimTypes.DateOfBirth) ?? "2000-01-01"),
            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
            EmailConfirmed = true
        };
        
        var resultReg = await userManager.CreateAsync(user);
        if (!resultReg.Succeeded) return BadRequest();
        
        var userFromDb = await userManager.FindByNameAsync(user.UserName);
        if (userFromDb == null)
            return BadRequest();
        await userManager.AddToRoleAsync(userFromDb, "Passenger");
        
        var identityResult = await userManager.AddLoginAsync(user, info);
        if (!identityResult.Succeeded) return BadRequest();
        
        await signInManager.SignInAsync(user, false);
        logger.LogInformation($"ExternalLoginCallback: {user.UserName}. Email {user.Email}");
        return Redirect(returnUrl);
    }
}