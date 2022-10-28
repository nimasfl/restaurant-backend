using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class NotAcceptableException : AppException
{
    public NotAcceptableException(string message = "")
        : base(HttpStatusCode.NotAcceptable, message)
    {}
}
