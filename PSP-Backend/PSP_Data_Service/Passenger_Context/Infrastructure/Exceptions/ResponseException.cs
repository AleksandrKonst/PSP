namespace PSP_Data_Service.Passenger_Context.Infrastructure.Exceptions;

public class ResponseException(string? message, string? errorCode) : Exception(message)
{
    public string? ErrorCode { get; } = errorCode;
}