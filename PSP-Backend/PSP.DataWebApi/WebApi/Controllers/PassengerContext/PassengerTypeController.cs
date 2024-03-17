using System.Dynamic;
using System.Text.Json;
using Application.DTO.PassengerContextDTO;
using Application.MediatR.Commands.PassengerTypeCommands;
using Application.MediatR.Queries.PassengerTypeQueries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Filters;
using WebApi.Infrastructure;

namespace WebApi.Controllers.PassengerContext;

[Authorize]
[ApiController]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("v{version:apiVersion}/[controller]")]
public class PassengerTypeController(IMediator mediator, IDistributedCache distributedCache) : ControllerBase

{
    [HttpGet]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, int index = 0, int count = 10)
    {
        var requestDateTime = DateTime.Now;
        
        var queryCount = new GetPassengerTypeCount.Query(); 
        var total = await mediator.Send(queryCount, cancellationToken);

        dynamic result = new ExpandoObject();
        
        IEnumerable<DocumentTypeDTO>? passengerTypeCache = null;
        var passengerTypeCacheString = await distributedCache.GetStringAsync($"PassengerType-{index}-{count}");
        if (passengerTypeCacheString != null) passengerTypeCache = JsonSerializer.Deserialize<IEnumerable<DocumentTypeDTO>>(passengerTypeCacheString);
        
        if (passengerTypeCache == null)
        {
            var queryPassenger = new GetPassengerTypes.Query(index, count);
            var passengerTypes = await mediator.Send(queryPassenger, cancellationToken);
        
            passengerTypeCacheString = JsonSerializer.Serialize(passengerTypes.Result);
            distributedCache.SetStringAsync($"PassengerType-{index}-{count}", passengerTypeCacheString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            });
            
            result.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                links = PaginationService.PaginateAsDynamic(HttpContext.Request.Path, index, count, total.Result)
            };
            result.passengers = passengerTypes.Result;
        }
        else
        {
            result.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                links = PaginationService.PaginateAsDynamic(HttpContext.Request.Path, index, count, total.Result)
            };
            result.passengers = passengerTypeCache;
        }
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var queryPassenger = new GetPassengerTypeById.Query(id);
        var passengerType = await mediator.Send(queryPassenger, cancellationToken);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.passenger = passengerType.Result;
        
        return Ok(response);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] PassengerTypeDTO passengerType, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
    
        var command = new CreatePassengerType.Command(passengerType);
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Тип пассажира добавлен"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка добавления типа пассажира", "PPC-000500");
    }
    
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] PassengerTypeDTO passengerType, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new UpdatePassengerType.Command(passengerType);
        var result = await mediator.Send(command, cancellationToken);
    
        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Тип пассажира изменен"
            };
            return Ok(response); 
        }
        throw new ResponseException("Ошибка изменения типа пассажира", "PPC-000500");
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
    
        var command = new DeletePassengerType.Command(id);
        var result = await mediator.Send(command, cancellationToken);
    
        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Тип пассажира удален"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка удаления типа пассажира", "PPC-000500");
    }
}