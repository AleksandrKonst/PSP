using System.Dynamic;
using Application.MediatR.Queries.SubsidizedRouteQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers.FlightContext;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("v{version:apiVersion}/[controller]")]
public class SubsidizedRouteController(IMediator mediator) : ControllerBase
{
    [HttpGet("{appendix}")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(short appendix, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;

        var query = new GetSubsidizedByAppendix.Query(appendix);
        var routes = await mediator.Send(query, cancellationToken);

        dynamic response = new ExpandoObject();
        
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.routes = routes.Result;
        
        return Ok(response);
    }
}