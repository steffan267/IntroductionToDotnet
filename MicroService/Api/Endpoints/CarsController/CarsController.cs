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
    /*Asp.net pipeline configures a route to "root_url/Cars"
      This introduces a lot of underlying magic e.g: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
      
      An important note is that a new "scope" is created for each controller invocation: https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#service-lifetimes
      I.e: All services injected will be a new instance (dependent on the lifetime registration) per request.
    */

    //2:
    [HttpPost] //This is created under "POST:root_url/Cars"
    [ProducesResponseType(typeof(CreateCarResponse),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromServices] IHandler<CreateCarRequest,CreateCarResponse> handler, [FromBody] CreateCarRequest request)
    {
        /* 
           In startup.cs we have registered fluentValidation(framework) to validate before hitting the method -> FluentValidation: https://docs.fluentvalidation.net/en/latest/aspnet.html
        */

        var response = await handler.Invoke(request);

        //it is possible to do post validation here as well.
        if (response.Id is null)
        {
            _logger.LogError("Returned null when not possible");
            return Problem(); //there are many ways to do this. You can also use StatusCode() etc., but these are all available in the ControllerBase.
        }
        
        //TL;DR: Lean controller methods using dependency injection
        return Ok(response);
    }
    
    [HttpGet("{id}")] //This is created under "GET:domain/Cars/id"
    [ProducesResponseType(typeof(GetCarResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromServices] IHandler<GetCarRequest,GetCarResponse> handler, [FromServices] IValidator<GetCarRequest> validator, [FromRoute] string id) 
    {
        //the parameter/route named id is automatically aligned with what is in the HttpGet-attribute
        var request = new GetCarRequest() { Id = id };
        var result = await validator.ValidateAsync(request);
        
        return result.IsValid ? 
            Ok(await handler.Invoke(request)) : 
            UnprocessableEntity(result);
    }
}