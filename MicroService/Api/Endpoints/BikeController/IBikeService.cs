using Domain;

namespace Api.Endpoints.BikeController;

public interface IBikeService
{
    Task<Bike> Get(string id);
    Task Create(BikeRequest request);
}