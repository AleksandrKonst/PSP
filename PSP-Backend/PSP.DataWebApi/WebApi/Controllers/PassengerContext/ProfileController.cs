using System.Dynamic;
using Application.MediatR.Queries.PassengerQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers.PassengerContext;

[Authorize]
[ApiController]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("v{version:apiVersion}/[controller]")]
public class ProfileController(IMediator mediator) : ControllerBase
{
    [HttpGet("info")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetInfo(CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var query = new GetPassengerByFio.Query(User.FindFirst("name")!.Value, User.FindFirst("surname")!.Value, 
            User.FindFirst("patronymic")!.Value, DateOnly.Parse(User.FindFirst("birthday")!.Value));
        var passenger = await mediator.Send(query, cancellationToken);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.passenger = passenger.Result;
        
        return Ok(response);
    }
    
    [HttpGet("quota/{quotaBalancesYear}")]
    [RequestSizeLimit(8 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetQuotaInfo(int quotaBalancesYear, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        dynamic coupon = new ExpandoObject();
        
        var query = new GetPassengerQuota.Query(User.FindFirst("name")!.Value, User.FindFirst("surname")!.Value, 
            User.FindFirst("patronymic")!.Value, DateOnly.Parse(User.FindFirst("birthday")!.Value), quotaBalancesYear);
        coupon = (await mediator.Send(query, cancellationToken)).Result;
        
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.passengers = coupon;
        
        return Ok(response);
    }
}