using System.Dynamic;
using System.Text.Json;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Commands.QuotaCategoryCommands;
using Application.MediatR.Queries.QuotaCategoryQueries;
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
public class QuotaCategoryController(IMediator mediator, IDistributedCache distributedCache) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, int index = 0, int count = 10)
    {
        var requestDateTime = DateTime.Now;
        
        var queryCount = new GetQuotaCategoryCount.Query(); 
        var total = await mediator.Send(queryCount, cancellationToken);

        dynamic response = new ExpandoObject();
        
        IEnumerable<QuotaCategoryDTO>? quotaCategoryCache = null;
        var quotaCategoryCacheString = await distributedCache.GetStringAsync($"QuotaCategory-{index}-{count}");
        if (quotaCategoryCacheString != null) quotaCategoryCache = JsonSerializer.Deserialize<IEnumerable<QuotaCategoryDTO>>(quotaCategoryCacheString);
        
        if (quotaCategoryCache == null)
        {
            var query = new GetQuotaCategories.Query(index, count);
            var quotaCategory = await mediator.Send(query, cancellationToken);
            
            quotaCategoryCacheString = JsonSerializer.Serialize(quotaCategory.Result);
            distributedCache.SetStringAsync($"QuotaCategory-{index}-{count}", quotaCategoryCacheString, new DistributedCacheEntryOptions
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
            response.quotaCategories = quotaCategory.Result;
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
            response.quotaCategories = quotaCategoryCache;
        }
        
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetById(string code, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var query = new GetQuotaCategoryById.Query(code);
        var quotaCategory = await mediator.Send(query, cancellationToken);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.quotaCategory = quotaCategory.Result;
        
        return Ok(response);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] QuotaCategoryDTO quotaCategoryDto, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new CreateQuotaCategory.Command(quotaCategoryDto);
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Категория квоты добавлена"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка добавления категории квоты", "PPC-000500");
    }
    
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] QuotaCategoryDTO quotaCategoryDto, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new UpdateQuotaCategory.Command(quotaCategoryDto);
        var result = await mediator.Send(command, cancellationToken);

        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Категория квоты изменена"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка изменения категории квоты", "PPC-000500");
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(string code, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var command = new DeleteQuotaCategory.Command(code);
        var result = await mediator.Send(command, cancellationToken);

        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Категория квоты удалена"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка удаления категории квоты", "PPC-000500");
    }
}