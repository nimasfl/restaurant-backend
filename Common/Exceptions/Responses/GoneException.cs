using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class GoneException : AppException
{
    public GoneException(string message = "")
        : base(HttpStatusCode.Gone, message)
    {}
}
