namespace Api;

public interface IHandler<in TRequest, TResponse> where TRequest : class
{
    Task<TResponse> Invoke(TRequest request);
}