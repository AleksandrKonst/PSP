using System.Dynamic;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.PassengerContextDTO;
using PSP.Domain.Exceptions;
using PSP.DataWebApi.Infrastructure;
using PSP.DataApplication.MediatR.Commands.PassengerTypeCommands;
using PSP.DataApplication.MediatR.Queries.PassengerTypeQueries;
using PSP.DataWebApi.Contexts.Passenger_Context.DTO;
using PSP.DataWebApi.Filters;

namespace PSP.DataWebApi.Contexts.Passenger_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
public class PassengerTypeController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, int index = 0, int count = Int32.MaxValue)
    {
        var requestDateTime = DateTime.Now;
        
        var queryCount = new GetPassengerTypeCount.Query(); 
        var total = await mediator.Send(queryCount, cancellationToken);

        var queryPassenger = new GetPassengerTypes.Query(index, count);
        var passengerTypes = await mediator.Send(queryPassenger, cancellationToken);
        
        dynamic result = new ExpandoObject();
        
        result.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
            links = PaginationService.PaginateAsDynamic(HttpContext.Request.Path, index, count, total.Result)
        };
        result.passengers = passengerTypes.Result;
        
        return Ok(result);
    }
    
    [HttpGet("{id}")]
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
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] PostDocumentTypeDTO passengerType, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
    
        var command = new CreatePassengerType.Command(mapper.Map<PassengerTypeDTO>(passengerType));
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
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] PostDocumentTypeDTO passengerType, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new UpdatePassengerType.Command(mapper.Map<PassengerTypeDTO>(passengerType));
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