using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using PSP.DataWebApi.ARM_Context.DTO;
using PSP.DataWebApi.ARM_Context.Services.Interfaces;
using PSP.DataWebApi.Passenger_Context.Infrastructure.Filters;

namespace PSP.DataWebApi.ARM_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
public class ARMController(IARMService service) : ControllerBase
{
    [HttpPost("select")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> PostSelect([FromBody] IEnumerable<SelectPassengerRequestDTO>  selectPassengerRequests)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var passengers = await service.SelectAsync(selectPassengerRequests);
            
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