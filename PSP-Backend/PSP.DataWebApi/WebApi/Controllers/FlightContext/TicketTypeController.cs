using System.Dynamic;
using Application.DTO.FlightContextDTO;
using Application.MediatR.Commands.TicketTypeCommands;
using Application.MediatR.Queries.TicketTypeQueries;
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
public class TicketTypeController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, int index = 0, int count = Int32.MaxValue)
    {
        var requestDateTime = DateTime.Now;
        
        var queryCount = new GetTicketTypeCount.Query(); 
        var total = await mediator.Send(queryCount, cancellationToken);

        var queryPassenger = new GetTicketTypes.Query(index, count);
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
    public async Task<IActionResult> GetById(short code, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var query = new GetTicketTypeById.Query(code);
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