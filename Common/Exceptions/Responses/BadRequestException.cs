using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class BadRequestException : AppException
{
    public BadRequestException()
        : base(HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message)
        : base(HttpStatusCode.BadRequest, message)
    {
    }

    public BadRequestException(List<string> additionalData)
        : base(HttpStatusCode.BadRequest)
    {
        AdditionalData = additionalData;
    }

    public BadRequestException(string message, List<string> additionalData)
        : base(HttpStatusCode.BadRequest, message)
    {
        AdditionalData = additionalData;
    }
}
