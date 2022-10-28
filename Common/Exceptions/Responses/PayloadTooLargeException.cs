using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class PayloadTooLargeException : AppException
{
    public PayloadTooLargeException(string message = "")
        : base(HttpStatusCode.RequestEntityTooLarge, message)
    {}
}
