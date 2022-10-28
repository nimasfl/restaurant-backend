using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class NotFoundException : AppException
{
    public NotFoundException(string message = "")
        : base(HttpStatusCode.NotFound, message)
    {}
}
