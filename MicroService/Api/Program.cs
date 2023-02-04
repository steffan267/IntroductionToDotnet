using Api;
using Api.Endpoints.CarsController.CreateCar;
using Api.Endpoints.CarsController.GetCar;
using Api.Middleware;
using Infrastructure;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

/*
 * Try running the application and go to loaclhost:port/swagger
 */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
 configuring the industry standard logging framework https://serilog.net/
 This will make sure that whenever someone needs an ILogger injected, the serilog logger will be injected as the implementation for the standard logging interface
  
  Fancy examples for how to use serilog: https://autoproff.atlassian.net/wiki/spaces/APDM/pages/2287697943/Logging+the+Stormtrooper+way
*/
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() //will log everything to console
    .WriteTo.Seq("http://localhost:5341") //will log everything to a local seq instance https://datalust.co/seq
    .Enrich.FromLogContext().Enrich.WithProperty("appName", "sampleapp")
    .CreateLogger();

builder.Logging.AddSerilog();
/*
 * This will use the Options pattern: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0
 * Essentially it means we can always inject IOptions<EnvironmentVariables> into a service (and thereby having it easily mockable)
 * Also note that builder.Configuration uses code like:
 *                 configuration = new ConfigurationBuilder()
 *                  .AddEnvironmentVariables();
                    .AddJsonFile($"appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.development.json", optional: true)
                    
    Which essentially means: Take environment variables, override with app settings if present, override with app settings development if present (I believe this is the order)
    app.settings.development should always be .gitignored and is for local development
 */
builder.Services.Configure<EnvironmentVariables>(builder.Configuration);

builder.Services.AddScoped<IHandler<CreateCarRequest, CreateCarResponse>, CreateCarHandler>();
builder.Services.AddScoped<IHandler<GetCarRequest, GetCarResponse>, GetCarHandler>();

var app = builder.Build();

// possible to retrieve the configuration object during app start:
var configObject = app.Services.CreateScope().ServiceProvider.GetRequiredService<IOptions<EnvironmentVariables>>();

if (app.Environment.IsDevelopment()) // environment is read from environment variables
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); // order of middleware registration is important since it will determine the order middleware is executed: in this case we do authorization before running exception handling when a new request is received: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
app.UseExceptionHandler(new ExceptionHandlerOptions() { AllowStatusCode404Response = true, ExceptionHandler = ExceptionMiddleware.Invoke });

app.MapControllers();

app.Run();

/// <summary>
/// Partial is a broken keyword you should never use unless you know exactly why.
/// This however exists to enable WebApplicationTestory (a native testing method) to access this.
/// This is one out of the two options available. The other requires reflection and assembly referencing,
/// I do not like it
/// </summary>
public partial class Program
{
}
