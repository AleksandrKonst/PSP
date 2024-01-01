using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using PSP_Data_Service.Passenger_Context.DTO;
using PSP_Data_Service.Passenger_Context.Infrastructure;
using PSP_Data_Service.Passenger_Context.Services.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PassengerController(IPassengerService service) : ControllerBase
{
    [HttpGet]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(int index = 0, int count = Int32.MaxValue)
    {
        var requestDateTime = DateTime.Now;
        var passengers = await service.GetPassengersAsync(index, count);
        var total = await service.GetPassengersCountAsync();
        
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
        dynamic result = new ExpandoObject();
        var requestDateTime = DateTime.Now;
        
        try
        {
            var passenger = await service.GetPassengerByIdAsync(id);
            
            result.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
            };
            result.passenger = passenger;
        
            return Ok(result);
        }
        catch (Exception e)
        {
            result.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                message = e.Message
            };
            
            return BadRequest(result);
        }
    }
    
    [HttpPost]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] PassengerDTO passenger)
    {
        try
        {
            var result = await service.AddPassenger(passenger);
            if (result) 
                return Ok();
            return NotFound("Ошибка добавления пассажира");
        }
        catch (Exception e)
        {
            return NotFound("Ошибка добавления пассажира");
        }
    }
    
    [HttpPut]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] PassengerDTO passenger)
    {
        var result = await service.UpdatePassenger(passenger);
        if (result) 
            return Ok();
        return NotFound("Ошибка обновления пассажира");
    }
    
    [HttpDelete]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post(int id)
    {
        try
        {
            var result = await service.DeletePassenger(id);
            if (result) 
                return Ok();
            return NotFound("Ошибка добавления пассажира");
        }
        catch (Exception e)
        {
            return NotFound("Ошибка добавления пассажира");
        }
    }
}