using System.Net;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PSP_Data_Service.Passenger_Context.Infrastructure.Exceptions;

namespace PSP_Data_Service.Passenger_Context.Infrastructure.Filters;

public class ResponseExceptionFilter(ILogger<ResponseExceptionFilter> logger) : ExceptionFilterAttribute
{
    private readonly ILogger<ResponseExceptionFilter> _logger = logger;

    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ResponseException responseException)
        {
            var errorList = new List<dynamic>();
            
            dynamic exception = new ExpandoObject();
            if (responseException.ErrorCode != null) exception.code = responseException.ErrorCode;
            exception.message = responseException.Message;
            
            errorList.Add(exception);
            
            var result = new ObjectResult(new
            {
                trace_id = Guid.NewGuid().ToString(),
                errors = errorList
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            
            context.Result = result;
        }
    }
}