using Domain;
using Infrastructure;
using Infrastructure.SomeExternalApi;

namespace Api.Endpoints.BikeController;

public class BikeService : IBikeService
{
    private readonly IExternalApi _externalApi;

    public BikeService(IExternalApi externalApi)
    {
        _externalApi = externalApi;
    }
    
    public async Task<Bike> Get(string id)
    {
        //application services interact with Infrastructure layer and domain layer separately, i.e this is where
        // we map.
        var response = await _externalApi.Retrieve(id);
        var bike = new Bike() { Id = response.Id.ToString() };
        return bike;
    }

    public Task Create(BikeRequest request)
    {
        throw new NotImplementedException();
    }
}