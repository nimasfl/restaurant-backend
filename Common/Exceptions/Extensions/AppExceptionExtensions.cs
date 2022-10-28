namespace Restaurant.Common.Exceptions.Extensions;

public static class AppExceptionToResponse
{
    public static AppExceptionResponse ToResponse(this AppException exc)
    {
        return new AppExceptionResponse
        {
            statusCode = exc.StatusCode,
            message = exc.Message,
            error = exc.Error ?? "Unknown Exception",
            additionalData = exc.AdditionalData
        };
    }
}
