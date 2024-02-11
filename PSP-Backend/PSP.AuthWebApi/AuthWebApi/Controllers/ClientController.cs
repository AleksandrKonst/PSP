using AuthWebApi.DTO.ViewModels.Client;
using AuthWebApi.Models.Data;
using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthWebApi.Controllers;

[Authorize]
public class ClientController(AuthDbContext context, IMapper mapper) : Controller
{
    private const int PageSize = 9;
    
    [HttpGet]
    public async Task<IActionResult> Index(string search, int page = 1)
    {
        ViewBag.SelectedCategory = "clients";

        var clients = await context.Clients.Select(c => c.ClientId).ToListAsync();
        
        var indexViewModel = new IndexViewModel()
        {
            Search = search,
            MaxPage = clients.Count / PageSize + 1,
            Page = page,
            Clients = clients
        };
        
        return View(indexViewModel);
    }
}