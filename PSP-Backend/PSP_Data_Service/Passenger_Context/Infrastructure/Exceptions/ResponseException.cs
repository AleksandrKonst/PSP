namespace PSP_Data_Service.Passenger_Context.Infrastructure.Exceptions;

public class ResponseException : Exception
{
    public string? ErrorCode { get; }
    
    public ResponseException(string? message, string? errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }
}