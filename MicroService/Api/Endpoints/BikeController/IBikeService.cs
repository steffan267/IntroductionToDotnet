namespace Api.Endpoints.BikeController;

public interface IBikeService
{
    Task<Car> Get(string id);
    Task Create(CarRequest request);
}