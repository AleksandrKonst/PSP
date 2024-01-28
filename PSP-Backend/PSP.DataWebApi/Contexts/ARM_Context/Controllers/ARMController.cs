using System.Dynamic;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.ArmContextDTO.Delete;
using PSP.DataApplication.DTO.ArmContextDTO.Search;
using PSP.DataApplication.DTO.ArmContextDTO.Select;
using PSP.DataApplication.MediatR.Commands.ARMCommands;
using PSP.DataApplication.MediatR.Queries.ARMQueries;
using PSP.DataWebApi.Contexts.ARM_Context.DTO;
using PSP.DataWebApi.Filters;
using PSP.Domain.Exceptions;

namespace PSP.DataWebApi.Contexts.ARM_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
public class ARMController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("select")]
    [RequestSizeLimit(8 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> PostSelect([FromBody] IList<PostSelectPassengerRequestDTO>  selectPassengerRequests, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var query = new SelectPassengerQuotaCount.Query(mapper.Map<IList<SelectPassengerRequestDTO>>(selectPassengerRequests));
        var passengers = await mediator.Send(query, cancellationToken);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.passengers = passengers.Result;
        
        return Ok(response);
    }
    
    [HttpPost("insert")]
    [RequestSizeLimit(8 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> PostInsert([FromBody] InsertPassengerRequestDTO  insertPassengerRequests, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new InsertCouponEvent.Command(insertPassengerRequests);
        var passengers = await mediator.Send(command, cancellationToken);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.passengers = passengers.Result;
        
        return Ok(response);
    }
    
    [HttpPost("batch")]
    [RequestSizeLimit(24 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> BatchInsert([FromBody] IList<InsertPassengerRequestDTO>  insertPassengerRequests, CancellationToken cancellationToken)
    { 
        var responses = new List<dynamic>();

        for (int i = 0; i < insertPassengerRequests.Count; i++)
        {
            dynamic response = new ExpandoObject();
            var command = new InsertCouponEvent.Command(insertPassengerRequests.ElementAt(i));
            var passengers = await mediator.Send(command, cancellationToken);

            response.operation_id = i + 1;
            response.passenger = passengers.Result;
            responses.Add(response);
        }
        
        return Ok(responses);
    }
    
    [HttpPost("delete")]
    [RequestSizeLimit(8 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteInsert([FromBody] DeleteCouponEventRequestDTO insertPassengerRequests, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new DeleteCouponEvent.Command(insertPassengerRequests);
        var passengers = await mediator.Send(command, cancellationToken);
        
        response.couponEventsDeleted = passengers.Result;
        
        return Ok(response);
    }
    
    [HttpPost("search")]
    [RequestSizeLimit(8 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> SearchPassengerInsert([FromBody] SearchRequestDTO searchRequest, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        dynamic coupon = new ExpandoObject();
        
        if (searchRequest.SearchType == "passenger")
        {
            var query = new SearchPasseneger.Query(mapper.Map<SearchPassengerDTO>(searchRequest));
            coupon = (await mediator.Send(query, cancellationToken)).Result;
        }
        else if (searchRequest.SearchType == "ticket")
        {
            var query = new SearchTicket.Query(mapper.Map<SearchTicketDTO>(searchRequest));
            coupon = (await mediator.Send(query, cancellationToken)).Result;
        }
        else
        {
            throw new ResponseException("PFC-000403", "Неверный идентификатор поиска");
        }
        
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