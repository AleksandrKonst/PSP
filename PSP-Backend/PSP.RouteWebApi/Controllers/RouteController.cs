using System.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSP.DataWebApi.Filters;
using PSP.RouteApplication.MediatR.Queries;

namespace PSP.RouteWebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
public class RouteController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(string arrivePlace, string departPlace, string date, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        
        var queryPassenger = new GetSortedRoute.Query(arrivePlace, departPlace, DateTime.Parse(date).ToUniversalTime());
        var routes = await mediator.Send(queryPassenger, cancellationToken);
        
        dynamic response = new ExpandoObject();
        
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now
        };
        response.passengers = routes.Result;
        
        return Ok(response);
    }
}