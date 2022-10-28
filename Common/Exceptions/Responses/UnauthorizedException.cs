using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message = "")
        : base(HttpStatusCode.Unauthorized, message)
    {}
}
