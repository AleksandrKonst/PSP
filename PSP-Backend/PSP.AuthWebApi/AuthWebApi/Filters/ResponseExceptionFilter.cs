using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Net;
using AuthWebApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthWebApi.Filters;

public class ResponseExceptionFilter(ILogger<ResponseExceptionFilter> logger) : ExceptionFilterAttribute
{
    private readonly ILogger<ResponseExceptionFilter> _logger = logger;

    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ResponseException responseException:
            {
                var errorList = new List<dynamic>();
            
                dynamic exception = new ExpandoObject();
                if (responseException.ErrorCode != null) exception.code = responseException.ErrorCode;
                exception.message = responseException.Message;
                errorList.Add(exception);
            
                logger.LogError($"ErrorCode: {responseException.ErrorCode} | ErrorMessage: {responseException.Message}");
                
                var result = new ObjectResult(new
                {
                    trace_id = Guid.NewGuid().ToString(),
                    errors = errorList
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            
                context.Result = result;
                break;
            }
            default:
            {
                var errorList = new List<dynamic>();

                dynamic exception = new ExpandoObject();
                exception.code = "PFC-000500";
                exception.message = context.Exception.Message;
                errorList.Add(exception);
            
                logger.LogError($"ErrorCode: PFC-000500 | ErrorMessage: {context.Exception.Message}");
                
                var result = new ObjectResult(new
                {
                    trace_id = Guid.NewGuid().ToString(),
                    errors = errorList
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            
                context.Result = result;
                break;
            }
        }
    }
}