using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApi.Controllers;

public class AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
        IIdentityServerInteractionService interactionService)
    : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel(){ReturnUrl = returnUrl});
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        // get tenant info// check if the model is valid
        if (ModelState.IsValid)
        {
            // check if the user exists
            var user = await userManager.FindByNameAsync(vm.Username);
            if (user != null)
            {
                // check if the password is correct
                var signInResult = signInManager.PasswordSignInAsync(user, vm.Password, false, false).Result;
                if (signInResult.Succeeded)
                {
                    // redirect to the return url
                    if (vm.ReturnUrl != null)
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Username or password is incorrect");
            }
        }
        return Redirect(vm.ReturnUrl);
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
    
    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var user = new IdentityUser(vm.Username);
        var result = await userManager.CreateAsync(user, vm.Password);

        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, false);

            return Redirect(vm.ReturnUrl);
        }

        return View(vm);
    }
}