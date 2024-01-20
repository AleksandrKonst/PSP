using System.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSP.DataApplication.DTO;
using PSP.DataApplication.MediatR.Queries.ARMQueries;
using PSP.DataWebApi.Filters;

namespace PSP.DataWebApi.Contexts.ARM_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
public class ARMController(IMediator mediator) : ControllerBase
{
    [HttpPost("select")]
    [RequestSizeLimit(8 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> PostSelect([FromBody] IList<SelectPassengerRequestDTO>  selectPassengerRequests, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var query = new SelectPassengerQuotaCount.Query(selectPassengerRequests);
        var passengers = await mediator.Send(query, cancellationToken);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.passengers = passengers;
        
        return Ok(response);
    }
}