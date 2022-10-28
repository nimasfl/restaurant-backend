using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class ForbiddenException : AppException
{
    public ForbiddenException(string message = "")
        : base(HttpStatusCode.Forbidden, message)
    {}
}
