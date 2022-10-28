using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class BadGatewayException : AppException
{
    public BadGatewayException(string message = "")
        : base(HttpStatusCode.BadGateway, message)
    {}
}
