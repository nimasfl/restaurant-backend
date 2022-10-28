using System.Net;

namespace Restaurant.Common.Exceptions;

public class AppException : Exception
{
    public int StatusCode { get; set; }
    public string? Error { get; set; }
    public List<string> AdditionalData { get; set; }

    protected AppException(HttpStatusCode statusCode, string? message = "")
        : base(message)
    {
        StatusCode = statusCode.GetHashCode();
        Error = statusCode.ToString();
        AdditionalData = new List<string>();
    }
    protected AppException(HttpStatusCode statusCode, string message, List<string> additionalData)
        : base(message)
    {
        StatusCode = statusCode.GetHashCode();
        Error = statusCode.ToString();
        AdditionalData = additionalData;
    }
    
    protected AppException(HttpStatusCode statusCode, List<string> additionalData)
        : base("")
    {
        StatusCode = statusCode.GetHashCode();
        Error = statusCode.ToString();
        AdditionalData = additionalData;
    }
    
}
