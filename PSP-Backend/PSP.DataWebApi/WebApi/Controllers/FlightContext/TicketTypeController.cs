using System.Dynamic;
using System.Text.Json;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Commands.TicketTypeCommands;
using Application.MediatR.Queries.TicketTypeQueries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Filters;
using WebApi.Infrastructure;

namespace WebApi.Controllers.FlightContext;

[Authorize]
[ApiController]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("v{version:apiVersion}/[controller]")]
public class TicketTypeController(IMediator mediator, IDistributedCache distributedCache) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, int index = 0, int count = 10)
    {
        var requestDateTime = DateTime.Now;
        
        var queryCount = new GetTicketTypeCount.Query(); 
        var total = await mediator.Send(queryCount, cancellationToken);

        dynamic response = new ExpandoObject();
        
        IEnumerable<TicketTypeDTO>? ticketTypeCache = null;
        var ticketTypeCacheString = await distributedCache.GetStringAsync($"TicketType-{index}-{count}");
        if (ticketTypeCacheString != null) ticketTypeCache = JsonSerializer.Deserialize<IEnumerable<TicketTypeDTO>>(ticketTypeCacheString);
        
        if (ticketTypeCache == null)
        {
            var query = new GetTicketTypes.Query(index, count);
            var ticketTypes = await mediator.Send(query, cancellationToken);
            
            ticketTypeCacheString = JsonSerializer.Serialize(ticketTypes.Result);
            distributedCache.SetStringAsync($"TicketType-{index}-{count}", ticketTypeCacheString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            });
            
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                links = PaginationService.PaginateAsDynamic(HttpContext.Request.Path, index, count, total.Result)
            };
            response.ticketTypes = ticketTypes.Result;
        }
        else
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                links = PaginationService.PaginateAsDynamic(HttpContext.Request.Path, index, count, total.Result)
            };
            response.ticketTypes = ticketTypeCache;
        }
        
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetById(short code, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var query = new GetTicketTypeById.Query(code);
        var ticketType = await mediator.Send(query, cancellationToken);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.ticketType = ticketType.Result;
        
        return Ok(response);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] TicketTypeDTO ticketTypeDto, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new CreateTicketType.Command(ticketTypeDto);
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Тип билета добавлен"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка добавления типа билета", "PPC-000500");
    }
    
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] TicketTypeDTO ticketTypeDto, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new UpdateTicketType.Command(ticketTypeDto);
        var result = await mediator.Send(command, cancellationToken);

        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Тип билета изменен"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка изменения типа билета", "PPC-000500");
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(short code, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var command = new DeleteTicketType.Command(code);
        var result = await mediator.Send(command, cancellationToken);

        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Тип билет удален"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка удаления типа билета", "PPC-000500");
    }
}