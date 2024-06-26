using System.Text;
using AuthWebApi.DTO.ViewModels.Client;
using AuthWebApi.Models;
using AuthWebApi.Models.Data;
using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthWebApi.Controllers;

[Authorize(Roles = "Admin")]
public class ClientController(IClientStore clientStore, AuthDbContext context, IMapper mapper, ILogger<AuthController> logger) : Controller
{
    private const int PageSize = 10;
    
    [HttpGet]
    public async Task<IActionResult> Index(string? search, int page = 1)
    {
        ViewBag.SelectedCategory = "clients";

        var clients = await context.Clients
            .Where(c => search == null || c.ClientId.ToLower().Contains(search.ToLower()))
            .Select(c => c.ClientId)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .AsNoTracking()
            .ToListAsync();
        
        var indexViewModel = new IndexViewModel()
        {
            Search = search ?? "",
            MaxPage = clients.Count / PageSize + 1,
            Page = page,
            Clients = clients
        };
        
        return View(indexViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> ExportCsv()
    {
        var clients = await context.Clients.ToListAsync();

        var builder = new StringBuilder();
        builder.AppendLine("ClientId,ClientSecrets,RequirePkce,RequireClientSecret,RedirectUris,AllowedScopes,AllowAccessTokensViaBrowser,AllowOfflineAccess,RequireConsent,PostLogoutRedirectUris,AllowedCorsOrigins");

        foreach (var client in clients)
        {
            client.MapDataFromEntity();
            builder.AppendLine($"{client.ClientId},{string.Join(":",client.Client.ClientSecrets.Select(c => c.Value))},{client.Client.RequirePkce}," +
                               $"{client.Client.RequireClientSecret},{(client.Client.RedirectUris == null ? null : string.Join(":",client.Client.RedirectUris))}," +
                               $"{(client.Client.AllowedScopes == null ? null : string.Join(":",client.Client.AllowedScopes))},{client.Client.AllowAccessTokensViaBrowser}," +
                               $"{client.Client.AllowOfflineAccess},{client.Client.RequireConsent},{(client.Client.PostLogoutRedirectUris == null ? null : string.Join(":",client.Client.PostLogoutRedirectUris))}," +
                               $"{(client.Client.AllowedCorsOrigins == null ? null : string.Join(":",client.Client.AllowedCorsOrigins))}");
        }
        
        return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "clients.csv");
    }
    
    [HttpGet]
    public async Task<IActionResult> Info(string id)
    {
        ViewBag.SelectedCategory = "clients";
        var client = await clientStore.FindClientByIdAsync(id);

        if (client == null)
        {
            return NoContent();
        }
        
        return View(mapper.Map<InfoViewModel>(client));
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        ViewBag.SelectedCategory = "clients";
        var client = await clientStore.FindClientByIdAsync(id);

        if (client == null)
        {
            return NoContent();
        }
        
        return View(mapper.Map<DeleteViewModel>(client));
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        ViewBag.SelectedCategory = "clients";
        var client = await context.Clients.FirstOrDefaultAsync(c => c.ClientId == id);

        if (client == null)
        {
            return NoContent();
        }

        var result = context.Clients.Remove(client);
        await context.SaveChangesAsync();
        
        logger.LogInformation($"Delete: {client.ClientId}");
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        ViewBag.SelectedCategory = "clients";
        var client = await clientStore.FindClientByIdAsync(id);
        
        if (client == null)
        {
            return NoContent();
        }

        var editClient = new EditViewModel()
        {
            ClientId = client.ClientId,
            AllowedGrantTypes = string.Join(" ",client.AllowedGrantTypes),
            RequirePkce = client.RequirePkce,
            RequireClientSecret = client.RequireClientSecret,
            RedirectUris = client.RedirectUris == null ? null : string.Join(" ",client.RedirectUris),
            AllowedScopes = client.AllowedScopes == null ? null : string.Join(" ",client.AllowedScopes),
            AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
            AllowOfflineAccess = client.AllowOfflineAccess,
            RequireConsent = client.RequireConsent,
            ClientSecrets =  client.ClientSecrets == null ? null : string.Join(" ",client.ClientSecrets.Select(c => c.Value)),
            PostLogoutRedirectUris =  client.PostLogoutRedirectUris == null ? null : string.Join(" ",client.PostLogoutRedirectUris),
            AllowedCorsOrigins =  client.AllowedCorsOrigins == null ? null : string.Join(" ",client.AllowedCorsOrigins)
        };
        
        return View(editClient);
    }
    
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(EditViewModel viewModel)
    {
        ViewBag.SelectedCategory = "clients";
        
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Model Error");
            return View(viewModel);
        }
        
        var client = await clientStore.FindClientByIdAsync(viewModel.ClientId);
        
        if (client == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(viewModel);
        }

        var secrets = viewModel.ClientSecrets?.Split(" ").Select(secret => new Secret(secret)).ToList();

        client.ClientId = viewModel.ClientId;
        client.AllowedGrantTypes = viewModel.AllowedGrantTypes.Split(" ");
        client.RequirePkce = viewModel.RequirePkce;
        client.RequireClientSecret = viewModel.RequireClientSecret;
        client.RedirectUris = viewModel.RedirectUris?.Split(" ");
        client.AllowedScopes = viewModel.AllowedScopes?.Split(" ");
        client.AllowAccessTokensViaBrowser = viewModel.AllowAccessTokensViaBrowser;
        client.AllowOfflineAccess = viewModel.AllowOfflineAccess;
        client.RequireConsent = viewModel.RequireConsent;
        client.ClientSecrets = secrets;
        client.PostLogoutRedirectUris = viewModel.PostLogoutRedirectUris?.Split(" ");
        client.AllowedCorsOrigins = viewModel.AllowedCorsOrigins?.Split(" ");

        var clientEntity = await context.Clients.FirstOrDefaultAsync(c => c.ClientId == viewModel.ClientId);
        if (clientEntity != null)
        {
            clientEntity.Client = client;
            clientEntity.AddDataToEntity();

            context.Clients.Update(clientEntity);
            await context.SaveChangesAsync();
        
            logger.LogInformation($"Edit: {client.ClientId}");
            return RedirectToAction(nameof(Edit), new {client.ClientId});
        }

        ModelState.AddModelError(string.Empty, "User not found");
        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.SelectedCategory = "clients";
        return View();
    }
    
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CreateViewModel viewModel)
    {
        ViewBag.SelectedCategory = "clients";
        
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Model Error");
            return View(viewModel);
        }

        if (viewModel is { RequireClientSecret: true, ClientSecrets: null })
        {
            ModelState.AddModelError(string.Empty, "Required Secret");
            return View(viewModel);
        }
        
        
        if (context.Clients.Any(c => c.ClientId == viewModel.ClientId))
        {
            ModelState.AddModelError(string.Empty, "Client Exist");
            return View(viewModel);
        }

        var secrets = viewModel.ClientSecrets != null ? viewModel.ClientSecrets.Split(" ").Select(secret => new Secret(secret)).ToList() : new List<Secret>();

        var client = new Client()
        {
            ClientId = viewModel.ClientId,
            AllowedGrantTypes = viewModel.AllowedGrantTypes.Split(" "),
            RequirePkce = viewModel.RequirePkce,
            RequireClientSecret = viewModel.RequireClientSecret,
            RedirectUris = viewModel.RedirectUris?.Split(" "),
            AllowedScopes = viewModel.AllowedScopes?.Split(" "),
            AllowAccessTokensViaBrowser = viewModel.AllowAccessTokensViaBrowser,
            AllowOfflineAccess = viewModel.AllowOfflineAccess,
            RequireConsent = viewModel.RequireConsent,
            ClientSecrets = secrets,
            PostLogoutRedirectUris = viewModel.PostLogoutRedirectUris?.Split(" "),
            AllowedCorsOrigins = viewModel.AllowedCorsOrigins?.Split(" ")
        };

        var clientEntity = new ClientEntity()
        {
            Client = client
        };
        clientEntity.AddDataToEntity();
        
        var result = await context.Clients.AddAsync(clientEntity);
        await context.SaveChangesAsync();
        
        logger.LogInformation($"Create: {client.ClientId}");
        return RedirectToAction(nameof(Create));
    }
}