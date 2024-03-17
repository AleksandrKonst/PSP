using System.Dynamic;
using System.Text.Json;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Commands.SubsidizedRouteCommands;
using Application.MediatR.Queries.SubsidizedRouteQueries;
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
public class SubsidizedRouteController(IMediator mediator, IDistributedCache distributedCache) : ControllerBase
{
    [HttpGet("appendix/{appendix}")]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(short appendix, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;

        dynamic response = new ExpandoObject();
        
        IEnumerable<SubsidizedRouteDTO>? subsidizedRouteCache = null;
        var subsidizedRouteCacheString = await distributedCache.GetStringAsync($"SubsidizedRouteByAppendix-{appendix}");
        if (subsidizedRouteCacheString != null) subsidizedRouteCache = JsonSerializer.Deserialize<IEnumerable<SubsidizedRouteDTO>>(subsidizedRouteCacheString);
        
        if (subsidizedRouteCache == null)
        {
            var query = new GetSubsidizedRouteByAppendix.Query(appendix);
            var routes = await mediator.Send(query, cancellationToken);
            
            subsidizedRouteCacheString = JsonSerializer.Serialize(routes.Result);
            distributedCache.SetStringAsync($"SubsidizedRouteByAppendix-{appendix}", subsidizedRouteCacheString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            });
            
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
            };
            response.routes = routes.Result;
        }
        else
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
            };
            response.routes = subsidizedRouteCache;
        }
        return Ok(response);
    }
    
    [HttpGet]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, int index = 0, int count = 10)
    {
        var requestDateTime = DateTime.Now;
        
        var queryCount = new GetSubsidizedRouteCount.Query(); 
        var total = await mediator.Send(queryCount, cancellationToken);

        dynamic response = new ExpandoObject();
        
        IEnumerable<SubsidizedRouteDTO>? subsidizedRouteCache = null;
        var subsidizedRouteCacheString = await distributedCache.GetStringAsync($"SubsidizedRoute-{index}-{count}");
        if (subsidizedRouteCacheString != null) subsidizedRouteCache = JsonSerializer.Deserialize<IEnumerable<SubsidizedRouteDTO>>(subsidizedRouteCacheString);
        
        if (subsidizedRouteCache == null)
        {
            var query = new GetSubsidizedRoutes.Query(index, count);
            var subsidizedRoutes = await mediator.Send(query, cancellationToken);
            
            subsidizedRouteCacheString = JsonSerializer.Serialize(subsidizedRoutes.Result);
            distributedCache.SetStringAsync($"SubsidizedRoute-{index}-{count}", subsidizedRouteCacheString, new DistributedCacheEntryOptions
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
            response.route = subsidizedRoutes.Result;
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
            response.route = subsidizedRouteCache;
        }
        
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetById(long code, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var query = new GetSubsidizedRouteById.Query(code);
        var subsidizedRoute = await mediator.Send(query, cancellationToken);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.route = subsidizedRoute.Result;
        
        return Ok(response);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] SubsidizedRouteDTO subsidizedRouteDto, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new CreateSubsidizedRoute.Command(subsidizedRouteDto);
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Субсидированный маршрут добавлен"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка добавления субсидируемого маршрута", "PPC-000500");
    }
    
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] SubsidizedRouteDTO subsidizedRouteDto, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new UpdateSubsidizedRoute.Command(subsidizedRouteDto);
        var result = await mediator.Send(command, cancellationToken);

        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Субсидируемый маршрут изменен"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка изменения субсидируемого маршрута", "PPC-000500");
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(long code, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var command = new DeleteSubsidizedRoute.Command(code);
        var result = await mediator.Send(command, cancellationToken);

        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Субсидируемый маршрут удален"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка удаления субсидируемого маршрута", "PPC-000500");
    }
}