using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class NotImplementedAppException : AppException
{
    public NotImplementedAppException(string message = "")
        : base(HttpStatusCode.NotImplemented, message)
    {}
}
