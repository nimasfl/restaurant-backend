using Microsoft.AspNetCore.Builder;
using Restaurant.Common.Exceptions.Middlewares;

namespace Restaurant.Common;

public static class ApplicationBuilderExtensions
{
    public static void UseAppExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<AppExceptionHandlerMiddleware>();
    }

    public static void UseAllowedCors(this IApplicationBuilder app)
    {
        app.UseCors(policyBuilder =>
        {
            policyBuilder.AllowAnyHeader();
            policyBuilder.AllowAnyMethod();
            policyBuilder.AllowAnyOrigin();
        });
    }
}
