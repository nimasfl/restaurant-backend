using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class UnsupportedMediaTypeException : AppException
{
    public UnsupportedMediaTypeException(string message = "")
        : base(HttpStatusCode.UnsupportedMediaType, message)
    {}
}
