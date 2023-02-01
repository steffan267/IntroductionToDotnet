using Api.Endpoints.CarsController.CreateCar;
using Api.Endpoints.CarsController.GetCar;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.CarsController;

[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private readonly ILogger<CarsController> _logger;

    public CarsController(ILogger<CarsController> logger)
    {
        _logger = logger;
    }
    //1:
    /*Asp.net pipeline configures a route to "domain/Cars"
      This introduces a lot of underlying magic e.g: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
      
      An important note is that a new "scope" is created for each controller invocation: https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#service-lifetimes
      I.e: All services injected will be a new instance (dependent on the lifetime registration) per request.
    */

    //2:
    [HttpPost] //This is created under "POST:domain/Cars"
    [ProducesResponseType(typeof(CreateCarResponse),StatusCodes.Status201Created)]
    public async Task<CreateCarResponse> Create([FromServices] IHandler<CreateCarRequest,CreateCarResponse> handler, [FromServices] IValidator<CreateCarRequest> validator, CreateCarRequest request)
    {
        /* It is entirely possible to use the native asp.net framework to do validation, so that it happens before code is executed in this method.
           Here I am using a framework called FluentValidation: https://docs.fluentvalidation.net/en/latest/aspnet.html
           And we will invoke it manually
        */
        await validator.ValidateAndThrowAsync(request); 
        
        var response = await handler.Invoke(request);

        //it is possible to do post validation here as well.
        if (response.Id is null)
        {
            _logger.LogError("Returned null when not possible");
            Problem(); //there are many ways to do this. You can also use StatusCode() etc., but these are all available in the ControllerBase.
        }
        
        //TL;DR: Lean controller methods using dependency injection
        return response;
    }
    
    [HttpGet("{id}")] //This is created under "GET:domain/Cars/id"
    [ProducesResponseType(typeof(GetCarResponse),StatusCodes.Status200OK)]
    public async Task<GetCarResponse> Create([FromServices] IHandler<GetCarRequest,GetCarResponse> handler, [FromServices] IValidator<GetCarRequest> validator, string id) 
    {
        //the parameter/argument named id is automatically aligned with what is in the HttpGet-attribute (if the names align.) The program will explode runtime - if they do not match
        var request = new GetCarRequest() { Id = id };
        await validator.ValidateAndThrowAsync(request); 
        
        var response = await handler.Invoke(request);

        return response;
    }
}