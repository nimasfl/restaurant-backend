using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Restaurant.Common.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant.App.Data.Contexts;
using Restaurant.Common.Authentication;
using Serilog.Sinks.SystemConsole.Themes;

namespace Restaurant.App;

public class Startup
{
    private readonly WebApplicationBuilder _builder;
    private WebApplication? _app;
    private string corsPolicy;

    public Startup(WebApplicationBuilder builder)
    {
        _builder = builder;
        _app = null;
        corsPolicy = "CORS_POLICY";
    }

    public void ConfigureServices()
    {
        ConfigureLogger();
        _builder.Host.UseSerilog();
        _builder.Services.AddDbContext<RestaurantDbContext>(options =>
        {
            options.UseNpgsql(_builder.Configuration.GetConnectionString("Restaurant"));
        });
        _builder.Services.AddCorsPolicy(corsPolicy);
        _builder.Services.AddControllersWithModelValidation(null);
        _builder.Services.AddEndpointsApiExplorer();
        _builder.Services.AddSwaggerGenWithBearerAuth();
        _builder.Services.AddInjectableAsSingleton();
        _builder.Services.AddInjectableAsTransient();
        _builder.Services.AddInjectableAsScoped();
        _builder.Services.AddAutoMapper();
        _builder.Services.AddJwtAuthentication(_builder.Configuration["Jwt:Key"]);
        _app = _builder.Build();
    }

    public void Configure()
    {
        if (_app == null)
        {
            throw new Exception("App is not initialized");
        }

        _app.UseCors(corsPolicy);
        _app.UseAppExceptionHandler();
        _app.UseAuthentication();
        _app.UseAuthorization();
        _app.UseAllowedCors();
        _app.UseSwagger();
        _app.UseSwaggerUI();
        _app.MapControllers();
        _app.Run();
    }

    private void ConfigureLogger()
    {
        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .WriteTo.Console(
                outputTemplate:
                "[{ThreadId}] {Timestamp:yyyy/MM/dd HH:mm:ss.fff} [{Level:u3}] {Message}{NewLine}{Exception}",
                theme: AnsiConsoleTheme.Code);
        Log.Logger = loggerConfiguration.CreateLogger();
    }
}
