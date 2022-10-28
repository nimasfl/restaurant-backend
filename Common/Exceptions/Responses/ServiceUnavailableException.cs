using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class ServiceUnavailableException : AppException
{
    public ServiceUnavailableException(string message = "")
        : base(HttpStatusCode.ServiceUnavailable, message)
    {}
}
