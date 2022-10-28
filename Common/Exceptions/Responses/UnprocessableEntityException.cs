using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class UnprocessableEntityException : AppException
{
    public UnprocessableEntityException(string message = "")
        : base(HttpStatusCode.UnprocessableEntity, message)
    {}
}
