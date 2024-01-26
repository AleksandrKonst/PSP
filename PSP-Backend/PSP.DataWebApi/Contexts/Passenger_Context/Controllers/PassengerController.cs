using MediatR;
using System.Dynamic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PSP.Domain.Exceptions;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.PassengerContextDTO;
using PSP.DataWebApi.Infrastructure;
using PSP.DataApplication.Mediatr.Commands.PassengerCommands;
using PSP.DataApplication.Mediatr.Queries.PassengerQueries;
using PSP.DataWebApi.Contexts.Passenger_Context.DTO;
using PSP.DataWebApi.Filters;

namespace PSP.DataWebApi.Contexts.Passenger_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
public class PassengerController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, int index = 0, int count = Int32.MaxValue)
    {
        var requestDateTime = DateTime.Now;
        
        var queryCount = new GetPassengerCount.Query(); 
        var total = await mediator.Send(queryCount, cancellationToken);

        var queryPassenger = new GetPassengers.Query(index, count);
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
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var query = new GetPassengerById.Query(id);
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
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] PostPassengerDTO passengerDto, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new CreatePassenger.Command(mapper.Map<PassengerDTO>(passengerDto));
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Пассажир добавлен"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка добавления пассажира", "PPC-000500");
    }
    
    [HttpPut]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] PostPassengerDTO passengerDto, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new UpdatePassenger.Command(mapper.Map<PassengerDTO>(passengerDto));
        var result = await mediator.Send(command, cancellationToken);

        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Пассажир изменен"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка изменения пассажира", "PPC-000500");
    }

    [HttpDelete]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var command = new DeletePassenger.Command(id);
        var result = await mediator.Send(command, cancellationToken);

        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Пассажир удален"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка удаления пассажира", "PPC-000500");
    }
}