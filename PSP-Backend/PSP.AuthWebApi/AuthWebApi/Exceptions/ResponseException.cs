namespace AuthWebApi.Exceptions;

public class ResponseException(string? message, string? errorCode) : Exception(message)
{
    public string? ErrorCode { get; } = errorCode;
}