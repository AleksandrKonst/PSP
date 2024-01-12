using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using PSP_Data_Service.Passenger_Context.DTO;
using PSP_Data_Service.Passenger_Context.Infrastructure;
using PSP_Data_Service.Passenger_Context.Infrastructure.Exceptions;
using PSP_Data_Service.Passenger_Context.Infrastructure.Filters;
using PSP_Data_Service.Passenger_Context.Services.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
public class PassengerController(IPassengerService service) : ControllerBase
{
    [HttpGet]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(int index = 0, int count = Int32.MaxValue)
    {
        var requestDateTime = DateTime.Now;
        var total = await service.GetPassengersCountAsync();
        var passengers = await service.GetPassengersAsync(index, count);
        
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
    
    [HttpGet("{id}")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetById(int id)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var passenger = await service.GetPassengerByIdAsync(id);
            
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
    public async Task<IActionResult> Post([FromBody] PassengerDTO passenger)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var result = await service.AddPassengerAsync(passenger);
            
        if (result)
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
    public async Task<IActionResult> Put([FromBody] PassengerDTO passenger)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var result = await service.UpdatePassengerAsync(passenger);

        if (result)
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
    public async Task<IActionResult> Delete(int id)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();

        var result = await service.DeletePassengerAsync(id);

        if (result)
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