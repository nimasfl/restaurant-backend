namespace Restaurant.Common.Exceptions;

public class AppExceptionResponse
{
    public int? statusCode { get; set; }
    public string? error { get; set; }
    public string? message { get; set; }
    public List<string> additionalData { get; set; } = new();
}
