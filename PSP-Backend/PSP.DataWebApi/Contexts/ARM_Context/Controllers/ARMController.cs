using System.Dynamic;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.ArmContextDTO.Select;
using PSP.DataApplication.MediatR.Commands.ARMCommands;
using PSP.DataApplication.MediatR.Queries.ARMQueries;
using PSP.DataWebApi.Contexts.ARM_Context.DTO;
using PSP.DataWebApi.Contexts.Passenger_Context.DTO;
using PSP.DataWebApi.Filters;

namespace PSP.DataWebApi.Contexts.ARM_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
public class ARMController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("select")]
    [RequestSizeLimit(8 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> PostSelect([FromBody] IList<PostSelectPassengerRequestDTO>  selectPassengerRequests, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var query = new SelectPassengerQuotaCount.Query(mapper.Map<IList<SelectPassengerRequestDTO>>(selectPassengerRequests));
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
    
    [HttpPost("insert")]
    [RequestSizeLimit(8 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> PostInsert([FromBody] InsertPassengerRequestDTO  insertPassengerRequests, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new InsertCouponEvent.Command(insertPassengerRequests);
        var passengers = await mediator.Send(command, cancellationToken);
            
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