using Microsoft.AspNetCore.Mvc;
using PSP_Data_Service.Passenger_Context.Services.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PassengerController : ControllerBase
{
    private readonly IPassengerService _service;
        
    public PassengerController(IPassengerService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetPassengersAsync(DateTime.Now);
        return Ok(result);
    }
}