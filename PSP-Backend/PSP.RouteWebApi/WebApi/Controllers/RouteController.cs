using System.Dynamic;
using System.Text.Json;
using Application.DTO;
using Application.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Filters;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("v{version:apiVersion}/[controller]")]
public class RouteController(IMediator mediator, IDistributedCache distributedCache) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(string departPlace, string arrivePlace, DateOnly date, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        
        dynamic response = new ExpandoObject();
        
        IEnumerable<FlightViewModel>? flightTypeCache = null;
        var flightTypeCacheString = await distributedCache.GetStringAsync($"Flights-{departPlace}-{arrivePlace}-{date}");
        if (flightTypeCacheString != null) flightTypeCache = JsonSerializer.Deserialize< IEnumerable<FlightViewModel>>(flightTypeCacheString);
        
        if (flightTypeCache == null)
        {
            var queryPassenger = new GetSortedRoute.Query(departPlace, arrivePlace, date);
            var routes = await mediator.Send(queryPassenger, cancellationToken);
        
            flightTypeCacheString = JsonSerializer.Serialize(routes.Result);
            distributedCache.SetStringAsync($"Flights-{departPlace}-{arrivePlace}-{date}", flightTypeCacheString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
            
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now
            };
            response.flights = routes.Result;
        }
        else
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now
            };
            response.flights = flightTypeCache;
        }
        return Ok(response);
    }
}