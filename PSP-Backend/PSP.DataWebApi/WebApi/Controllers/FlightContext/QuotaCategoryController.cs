using System.Dynamic;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Commands.QuotaCategoryCommands;
using Application.MediatR.Queries.QuotaCategoryQueries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Infrastructure;

namespace WebApi.Controllers.FlightContext;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("v{version:apiVersion}/[controller]")]
public class QuotaCategoryController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, int index = 0, int count = Int32.MaxValue)
    {
        var requestDateTime = DateTime.Now;
        
        var queryCount = new GetQuotaCategoryCount.Query(); 
        var total = await mediator.Send(queryCount, cancellationToken);

        var queryPassenger = new GetQuotaCategories.Query(index, count);
        var passengers = await mediator.Send(queryPassenger, cancellationToken);

        dynamic response = new ExpandoObject();
        
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
            links = PaginationService.PaginateAsDynamic(HttpContext.Request.Path, index, count, total.Result)
        };
        response.passengers = passengers.Result;
        
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