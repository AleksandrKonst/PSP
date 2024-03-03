using System.Dynamic;
using Application.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("v{version:apiVersion}/[controller]")]
public class RouteController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(string departPlace, string arrivePlace, DateOnly date, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        
        var queryPassenger = new GetSortedRoute.Query(departPlace, arrivePlace, date);
        var routes = await mediator.Send(queryPassenger, cancellationToken);
        
        dynamic response = new ExpandoObject();
        
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now
        };
        response.flights = routes.Result;
        
        return Ok(response);
    }
}