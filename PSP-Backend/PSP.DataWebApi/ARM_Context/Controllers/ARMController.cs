using Microsoft.AspNetCore.Mvc;
using PSP.DataWebApi.Passenger_Context.Infrastructure.Filters;

namespace PSP.DataWebApi.ARM_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
public class ARMController : ControllerBase
{
    
}