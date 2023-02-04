using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.BikeController;

[ApiController]
[Route("[controller]")]
public class BikesController : ControllerBase
{
    private readonly IBikeService _service;
    
    public BikesController(IBikeService service)
    {
        _service = service;
    }
    
    /*
     * Thoughts on v1:
     *      -  Constructor injecting of services does not make it clear that a new instance is injected per request invocation
     *      -  Constructor will quickly have too many parameters, if we have different services for different things.
     *              (and to mitigate this, one could be inclined to have a very large application service, which usually ends up annoying as well)
     *      -  Testing the constructor becomes more annoying, because you now have to always mock all arguments to the constructor, instead of just the method.
     *      -  Having multiple services injected in the constructor also adds a negative performance overhead for actions that doesn't need the given services,
     *              but the services still needs to be initialized. fx creating a DB connection for an action that your just need the current time will at at least 50ms in establishing db connection
     */

    [HttpGet("v1/{id}")] //This is created under "GET:domain/Bikes/v1/id"
    public async Task<Bike> Get(string id)
    {
        return await _service.Get(id);
    }
    
    /*
     * Thoughts on v2:
     *      -  Easy to test as we only need relevant elements
     *      -  will be less likely to fail if we forgot to register the relevant service being injected. (Injection is runtime failure)
     */
    
    [HttpGet("v2/{id}")] //This is created under "GET:domain/Bikes/v2/id"
    public async Task<Bike> Get([FromServices] IBikeService service, string id)
    {
        return await service.Get(id);
    }
}