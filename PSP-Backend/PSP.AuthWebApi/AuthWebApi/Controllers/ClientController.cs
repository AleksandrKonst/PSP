using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApi.Controllers;

[Authorize]
public class ClientController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewBag.SelectedCategory = "clients";
        
        return View();
    }
}