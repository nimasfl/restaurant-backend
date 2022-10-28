using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class ConflictException : AppException
{
    public ConflictException(string message = "")
        : base(HttpStatusCode.Conflict, message)
    {}
}
