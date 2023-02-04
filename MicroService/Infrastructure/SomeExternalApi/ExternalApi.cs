using System.Net;
using Flurl.Http;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Options;

namespace Infrastructure.SomeExternalApi;

public class ExternalApi : IExternalApi
{
    private readonly EnvironmentVariables _options;

    public ExternalApi(IOptions<EnvironmentVariables> options)
    {
        _options = options.Value;
    }
    
    public async Task<ApiResponseModel> Retrieve(string id)
    {
        //Here we use flurl to invoke an endpoint and convert the response.
        //Flurl is a fluent framework that has some nice testing capabilities as well
        // https://flurl.dev/
        
        var response = 
            await _options.EXTERNAL_API_BASEURL.PostJsonAsync(@"{ ""Name"": ""GG"" }")
                .ReceiveJson<ApiResponseModel>();

        if (response.Id is null)
        {
            throw new NotFoundException("Bike not found");
        }
        
        return response;
    }
    public async Task<ApiResponseModel> RetrieveFancy(string id)
    {
        //Here we use flurl to invoke an endpoint and convert the response.
        //Flurl is a fluent framework that has some nice testing capabilities as well
        // https://flurl.dev/
        
        var response = 
            await _options.EXTERNAL_API_BASEURL.PostJsonAsync(@"{ ""Name"": ""GG"" }");

        return await HandleResponse(response);
    }

    async Task<ApiResponseModel> HandleResponse(IFlurlResponse response) =>
        response switch
        {
            {StatusCode: (int)HttpStatusCode.Accepted} => await response.GetJsonAsync<ApiResponseModel>(),
            {StatusCode: (int)HttpStatusCode.BadRequest} => throw new ArgumentException($"Recieved {response.StatusCode} from {response.ResponseMessage.RequestMessage?.RequestUri} with message: {await response.GetStringAsync()}"),
            {StatusCode: >(int)HttpStatusCode.Accepted and < 300} => await response.GetJsonAsync<ApiResponseModel>(),
            {StatusCode: >400 and < 500} => throw new UnauthorizedAccessException(response.ResponseMessage.ReasonPhrase),
            _ => throw new NotFoundException("Bike not found"),
        };
}