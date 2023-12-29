using Microsoft.AspNetCore.Mvc;
using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Controllers;

[ApiController]
[Route("api")]
public class GenderTypeController : ControllerBase
{
    private readonly IGenderTypeRepository _typeRepository;
    
    public GenderTypeController(IGenderTypeRepository typeRepository)
    {
        this._typeRepository = typeRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenderType>>> GetAllStations()
    {
        return  Ok(await _typeRepository.GetAll());
    }
}