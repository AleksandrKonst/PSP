using System.Dynamic;
using System.Text.Json;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Commands.AirlineCommands;
using Application.MediatR.Queries.AirlineQueries;
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
public class AirlineController(IMediator mediator, IDistributedCache distributedCache) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, int index = 0, int count = 10)
    {
        var requestDateTime = DateTime.Now;
        
        var queryCount = new GetAirlineCount.Query(); 
        var total = await mediator.Send(queryCount, cancellationToken);

        dynamic response = new ExpandoObject();
        
        IEnumerable<AirlineDTO>? airlineCache = null;
        var airlineCacheString = await distributedCache.GetStringAsync($"Airline-{index}-{count}");
        if (airlineCacheString != null) airlineCache = JsonSerializer.Deserialize<IEnumerable<AirlineDTO>>(airlineCacheString);
        
        if (airlineCache == null)
        {
            var query = new GetAirlines.Query(index, count);
            var airlines = await mediator.Send(query, cancellationToken);
            
            airlineCacheString = JsonSerializer.Serialize(airlines.Result);
            distributedCache.SetStringAsync($"Airline-{index}-{count}", airlineCacheString, new DistributedCacheEntryOptions
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
            response.airline = airlines.Result;
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
            response.airlines = airlineCache;
        }
        return Ok(response);
    }
    
    [HttpGet("{code}")]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetById(string code, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var query = new GetAirlineById.Query(code);
        var airline = await mediator.Send(query, cancellationToken);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.airline = airline.Result;
        
        return Ok(response);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] AirlineDTO airlineDto, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new CreateAirline.Command(airlineDto);
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Перевозчик добавлен"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка добавления первозчика", "PPC-000500");
    }
    
    [HttpPut]
    [Authorize(Policy = "NotForPassenger")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] AirlineDTO airlineDto, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new UpdateAirline.Command(airlineDto);
        var result = await mediator.Send(command, cancellationToken);

        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Перевозчик изменен"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка изменения перевозчика", "PPC-000500");
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(string code, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var command = new DeleteAirline.Command(code);
        var result = await mediator.Send(command, cancellationToken);

        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Перевозчик удален"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка удаления перевозчика", "PPC-000500");
    }
}