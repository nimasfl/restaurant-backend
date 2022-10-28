using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class MethodNotAllowedException : AppException
{
    public MethodNotAllowedException(string message = "")
        : base(HttpStatusCode.MethodNotAllowed, message)
    {}
}
