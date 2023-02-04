namespace Infrastructure.SomeExternalApi;

public interface IExternalApi
{
    Task<ApiResponseModel> Retrieve(string id);
}