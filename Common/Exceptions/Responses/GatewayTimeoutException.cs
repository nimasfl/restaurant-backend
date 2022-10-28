using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class GatewayTimeoutException : AppException
{
    public GatewayTimeoutException(string message = "")
        : base(HttpStatusCode.GatewayTimeout, message)
    {}
}
