using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class PreconditionFailedException : AppException
{
    public PreconditionFailedException(string message = "")
        : base(HttpStatusCode.PreconditionFailed, message)
    {}
}
