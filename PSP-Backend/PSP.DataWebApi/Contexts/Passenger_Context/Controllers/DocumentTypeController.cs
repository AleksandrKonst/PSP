using System.Dynamic;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.PassengerContextDTO;
using PSP.DataApplication.MediatR.Commands.DocumentTypeCommands;
using PSP.DataApplication.MediatR.Queries.DocumentTypeQueries;
using PSP.DataWebApi.Contexts.Passenger_Context.DTO;
using PSP.DataWebApi.Filters;
using PSP.DataWebApi.Infrastructure;
using PSP.Domain.Exceptions;

namespace PSP.DataWebApi.Contexts.Passenger_Context.Controllers;

[ApiController]
[ApiVersion("1.0")]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
public class DocumentTypeController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, int index = 0, int count = Int32.MaxValue)
    {
        var requestDateTime = DateTime.Now;
        
        var queryCount = new GetDocumentTypeCount.Query(); 
        var total = await mediator.Send(queryCount, cancellationToken);

        var queryPassenger = new GetDocumentTypes.Query(index, count);
        var documentTypes = await mediator.Send(queryPassenger, cancellationToken);
        
        dynamic result = new ExpandoObject();
        
        result.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
            links = PaginationService.PaginateAsDynamic(HttpContext.Request.Path, index, count, total.Result)
        };
        result.passengers = documentTypes.Result;
        
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var queryPassenger = new GetDocumentTypeById.Query(id);
        var documentType = await mediator.Send(queryPassenger, cancellationToken);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.passenger = documentType.Result;
        
        return Ok(response);
    }
    
    [HttpPost]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] PostDocumentTypeDTO documentType, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
    
        var command = new CreateDocumentType.Command(mapper.Map<DocumentTypeDTO>(documentType));
        var result = await mediator.Send(command, cancellationToken);
            
        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Тип документа добавлен"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка добавления типа документа", "PPC-000500");
    }
    
    [HttpPut]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] PostDocumentTypeDTO documentType, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new UpdateDocumentType.Command(mapper.Map<DocumentTypeDTO>(documentType));
        var result = await mediator.Send(command, cancellationToken);
    
        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Тип документа изменен"
            };
            return Ok(response); 
        }
        throw new ResponseException("Ошибка изменения типа документа", "PPC-000500");
    }
    
    [HttpDelete]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
    
        var command = new DeleteDocumentType.Command(id);
        var result = await mediator.Send(command, cancellationToken);
    
        if (result.Result)
        {
            response.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                mesaage = "Тип документа удален"
            };
            return Ok(response);
        }
        throw new ResponseException("Ошибка удаления типа документа", "PPC-000500");
    }
}