using AuthWebApi.Models.Data;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace AuthWebApi.Models.Infrastructure;

public class ClientStore : IClientStore
{
    private readonly AuthDbContext _context;
 
    public ClientStore(AuthDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
    }
 
    public Task<Client> FindClientByIdAsync(string clientId)
    {
        var client = _context.Clients.First(t => t.ClientId == clientId);
        client.MapDataFromEntity();
        return Task.FromResult(client.Client);
    }
}