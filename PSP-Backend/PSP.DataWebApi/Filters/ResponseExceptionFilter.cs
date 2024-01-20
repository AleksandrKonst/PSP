using System.Dynamic;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PSP.Domain.Exceptions;

namespace PSP.DataWebApi.Filters;

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
        else if (context.Exception is ValidationException validationException)
        {
            var errorList = new List<dynamic>();

            foreach (var error in validationException.Errors)
            {
                dynamic exception = new ExpandoObject();
                exception.code = error.ErrorCode;
                exception.message = error.ErrorMessage;
                errorList.Add(exception);
            }
            
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