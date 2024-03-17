using System.Dynamic;
using System.Text.Json;
using Application.DTO.PassengerContextDTO;
using Application.MediatR.Commands.DocumentTypeCommands;
using Application.MediatR.Queries.DocumentTypeQueries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Filters;
using WebApi.Infrastructure;

namespace WebApi.Controllers.PassengerContext;

[Authorize]
[ApiController]
[TypeFilter(typeof(ResponseExceptionFilter))]
[Route("v{version:apiVersion}/[controller]")]
public class DocumentTypeController(IMediator mediator, IDistributedCache distributedCache) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, int index = 0, int count = 10)
    {
        var requestDateTime = DateTime.Now;
        
        var queryCount = new GetDocumentTypeCount.Query(); 
        var total = await mediator.Send(queryCount, cancellationToken);
        
        dynamic result = new ExpandoObject();
        
        IEnumerable<DocumentTypeDTO>? documentTypeCache = null;
        var documentTypeCacheString = await distributedCache.GetStringAsync($"DocumentType-{index}-{count}");
        if (documentTypeCacheString != null) documentTypeCache = JsonSerializer.Deserialize<IEnumerable<DocumentTypeDTO>>(documentTypeCacheString);
        
        if (documentTypeCache == null)
        {
            var query = new GetDocumentTypes.Query(index, count);
            var documentTypes = await mediator.Send(query, cancellationToken);
        
            documentTypeCacheString = JsonSerializer.Serialize(documentTypes.Result);
            distributedCache.SetStringAsync($"DocumentType-{index}-{count}", documentTypeCacheString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            });
            
            result.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                links = PaginationService.PaginateAsDynamic(HttpContext.Request.Path, index, count, total.Result)
            };
            result.documentTypes = documentTypes.Result;
        }
        else
        {
            result.service_data = new
            {
                request_id = Guid.NewGuid().ToString(),
                request_datetime = requestDateTime,
                response_datetime = DateTime.Now,
                links = PaginationService.PaginateAsDynamic(HttpContext.Request.Path, index, count, total.Result)
            };
            result.documentTypes = documentTypeCache;
        }
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    { 
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var query = new GetDocumentTypeById.Query(id);
        var documentType = await mediator.Send(query, cancellationToken);
            
        response.service_data = new
        {
            request_id = Guid.NewGuid().ToString(),
            request_datetime = requestDateTime,
            response_datetime = DateTime.Now,
        };
        response.documentType = documentType.Result;
        
        return Ok(response);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] DocumentTypeDTO documentType, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
    
        var command = new CreateDocumentType.Command(documentType);
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
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(1 * 1024)]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] DocumentTypeDTO documentType, CancellationToken cancellationToken)
    {
        var requestDateTime = DateTime.Now;
        dynamic response = new ExpandoObject();
        
        var command = new UpdateDocumentType.Command(documentType);
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
    [Authorize(Roles = "Admin")]
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