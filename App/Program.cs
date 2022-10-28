using Restaurant.App;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var startup = new Startup(builder);
    startup.ConfigureServices();
    startup.Configure();
}
catch (Exception e)
{
    Log.Fatal(e, "Exception happened during program execution");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
