using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class InternalServerErrorException : AppException
{
    public InternalServerErrorException(string message = "")
        : base(HttpStatusCode.InternalServerError, message)
    {}
}
