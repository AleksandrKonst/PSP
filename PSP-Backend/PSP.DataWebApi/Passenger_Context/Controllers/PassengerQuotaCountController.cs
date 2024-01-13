using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using PSP.DataWebApi.Passenger_Context.DTO;
using PSP.DataWebApi.Passenger_Context.Infrastructure;
using PSP.Infrastructure.Exceptions;
using PSP.DataWebApi.Passenger_Context.Infrastructure.Filters;
using PSP.DataWebApi.Passenger_Context.Services.Interfaces;

namespace PSP.DataWebApi.Passenger_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
public class PassengerQuotaCountController(IPassengerQuotaCountService service) : ControllerBase
{
    [HttpGet]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(int index = 0, int count = Int32.MaxValue)
    {
        var requestDateTime = DateTime.Now;
        var total = await service.GetPassengerQuotaCountLenghtAsync();
        var passengers = await service.GetPassengerQuotaCountsAsync(index, count);
        
        dynamic result = new ExpandoObject();
        
        result.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
            links = PaginationService.PaginateAsDynamic(HttpContext.Request.Path, index, count, total)
        };
        result.passengers = passengers;
        
        return Ok(result);
    }
    
    [HttpGet("{passengerId}/{quotaCategory}/{year}")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetById(long passengerId, string quotaCategory, string year)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var passenger = await service.GetPassengerQuotaCountByIdAsync(passengerId, quotaCategory, year);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.passenger = passenger;
        
        return Ok(response);
    }
    
    [HttpPost]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] PassengerQuotaCountDTO passengerQuotaCount)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var result = await service.AddPassengerQuotaCountAsync(passengerQuotaCount);
            
        if (result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Квота добавлена"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка добавления квоты", "PPC-000500");
    }
    
    [HttpPut]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] PassengerQuotaCountDTO passengerQuotaCount)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var result = await service.UpdatePassengerQuotaCountAsync(passengerQuotaCount);

        if (result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Квота изменена"
            };
            return Ok(response); 
        }
        throw new ResponseException("Ошибка изменения квоты", "PPC-000500");
    }

    [HttpDelete]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(long id)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var result = await service.DeletePassengerQuotaCountAsync(id);

        if (result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Квота удалена"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка удаления квоты", "PPC-000500");
    }
}