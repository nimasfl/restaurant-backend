using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class RequestTimeoutException : AppException
{
    public RequestTimeoutException(string message = "")
        : base(HttpStatusCode.RequestTimeout, message)
    {}
}
