using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Restaurant.Common.Exceptions.Extensions;

namespace Restaurant.Common.Exceptions.Middlewares;

public class AppExceptionHandlerMiddleware
{
    private ILogger<AppExceptionHandlerMiddleware> logger;
    private readonly RequestDelegate next;

    public AppExceptionHandlerMiddleware(RequestDelegate next, ILogger<AppExceptionHandlerMiddleware> logger)
    {
        this.logger = logger;
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AppException exception)
        {
            logger.LogError(exception, exception.Message);
            var result = exception.ToResponse();
            var json = JsonConvert.SerializeObject(result);
            context.Response.StatusCode = exception.StatusCode;
            await context.Response.WriteAsync(json);
        }
        catch (Exception exception)
        {
            logger.LogCritical(exception, exception?.Message);
            var appEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (appEnvironment == "Development")
            {
                throw;
            }
            var result = new AppExceptionResponse
            {
                error = "Unknown Exception",
                statusCode = -1,
                message = "خطای نامشخصی در سیستم رخ داده است"
            };
            var json = JsonConvert.SerializeObject(result);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(json);
        }
    }
}
