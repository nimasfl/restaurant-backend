using System.Net;

namespace Restaurant.Common.Exceptions.Responses;

public class MissDirectedException : AppException
{
    public MissDirectedException(string message = "")
        : base(HttpStatusCode.MisdirectedRequest, message)
    {}
}
