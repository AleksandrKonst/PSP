using System.Dynamic;
using System.Text.Json;
using Application.DTO.ArmContextDTO.Delete;
using Application.DTO.ArmContextDTO.Insert;
using Application.DTO.ArmContextDTO.Search;
using Application.DTO.ArmContextDTO.Select;
using Application.MediatR.Commands.ARMCommands;
using Application.MediatR.Queries.ARMQueries;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Filters;

namespace WebApi.Controllers.ARMContext;

[Authorize]
[ApiController]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("v{version:apiVersion}/[controller]")]
public class ARMController(IMediator mediator, IMapper mapper, IDistributedCache distributedCache) : ControllerBase
{
    [HttpPost("select")]
    [Authorize(Policy = "NotForPassenger")]
    [RequestSizeLimit(8 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> PostSelect([FromBody] IList<SelectPassengerRequestDTO>  selectPassengerRequests, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var query = new SelectPassengerQuotaCount.Query(selectPassengerRequests);
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
    [Authorize(Policy = "NotForPassenger")]
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
    [Authorize(Policy = "NotForPassenger")]
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
    [Authorize(Policy = "NotForPassenger")]
    [RequestSizeLimit(8 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteInsert([FromBody] DeleteCouponEventRequestDTO insertPassengerRequests, CancellationToken cancellationToken)
    { 
        dynamic response = new ExpandoObject();
        
        var command = new DeleteCouponEvent.Command(insertPassengerRequests);
        var passengers = await mediator.Send(command, cancellationToken);
        
        response.couponEventsDeleted = passengers.Result;
        
        return Ok(response);
    }
    
    [HttpPost("search")]
    [Authorize]
    [RequestSizeLimit(8 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> SearchPassengerInsert([FromBody] SearchRequestDTO searchRequest, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        dynamic coupon = new ExpandoObject();
        
        if (searchRequest.SearchType == "passenger")
        {
            SearchPassengerResponseDTO? searchPassengerResponse = null;
            var searchPassengerResponseString = await distributedCache.GetStringAsync(searchRequest.Name + searchRequest.Surname 
                + searchRequest.Patronymic + searchRequest.Gender + searchRequest.Birthdate);
            if (searchPassengerResponseString != null) searchPassengerResponse = JsonSerializer.Deserialize<SearchPassengerResponseDTO>(searchPassengerResponseString);
            
            if (searchPassengerResponse == null)
            {
                var query = new SearchByPassenger.Query(mapper.Map<SearchByPassengerDTO>(searchRequest));
                coupon = (await mediator.Send(query, cancellationToken)).Result;
                
                if (coupon != null)
                {
                    searchPassengerResponseString = JsonSerializer.Serialize(coupon);
                    await distributedCache.SetStringAsync(searchRequest.Name + searchRequest.Surname + searchRequest.Patronymic 
                                                          + searchRequest.Gender + searchRequest.Birthdate, searchPassengerResponseString, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                }
            }
            else
            {
                coupon = searchPassengerResponse;
            }
        }
        else if (searchRequest.SearchType == "ticket")
        {
            var query = new SearchByTicket.Query(mapper.Map<SearchByTicketDTO>(searchRequest));
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