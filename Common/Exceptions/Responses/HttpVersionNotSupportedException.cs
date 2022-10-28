using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class HttpVersionNotSupportedException : AppException
{
    public HttpVersionNotSupportedException(string message = "")
        : base(HttpStatusCode.HttpVersionNotSupported, message)
    {}
}
