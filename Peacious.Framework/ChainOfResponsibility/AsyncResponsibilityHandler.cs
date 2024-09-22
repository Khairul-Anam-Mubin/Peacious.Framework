namespace Peacious.Framework.ChainOfResponsibility;

public interface IAsyncResponsibilityHandler<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request);
}

public abstract class AAsyncResponsibilityHandler<TRequest, TResponse>
    : IAsyncResponsibilityHandler<TRequest, TResponse>
{
    private IAsyncResponsibilityHandler<TRequest, TResponse>? NextHandler { get; set; }

    protected abstract Task<bool> CanHandleAsync(TRequest request);
    protected abstract Task<TResponse> OnHandleAsync(TRequest request);

    public IAsyncResponsibilityHandler<TRequest, TResponse> SetNext(
        IAsyncResponsibilityHandler<TRequest, TResponse> nextHandler)
    {
        NextHandler = nextHandler;
        return this;
    }

    public async Task<TResponse> HandleAsync(TRequest request)
    {
        if (await CanHandleAsync(request))
        {
            return await OnHandleAsync(request);
        }

        if (NextHandler is not null)
        {
            return await NextHandler.HandleAsync(request);
        }

        throw new Exception("No handler satisfied to handle the request");
    }
}