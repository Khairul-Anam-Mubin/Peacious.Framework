namespace Peacious.Framework.ChainOfResponsibility;

public interface IResponsibilityHandler<TRequest, TResponse>
{
    TResponse Handle(TRequest request);
}

public abstract class AResponsibilityHandler<TRequest, TResponse>
    : IResponsibilityHandler<TRequest, TResponse>
{
    private IResponsibilityHandler<TRequest, TResponse>? NextHandler { get; set; }

    protected abstract bool CanHandle(TRequest request);
    protected abstract TResponse OnHandle(TRequest request);

    public IResponsibilityHandler<TRequest, TResponse> SetNext(
        IResponsibilityHandler<TRequest, TResponse> nextHandler)
    {
        NextHandler = nextHandler;
        return this;
    }

    public TResponse Handle(TRequest request)
    {
        if (CanHandle(request))
        {
            return OnHandle(request);
        }

        if (NextHandler is not null)
        {
            return NextHandler.Handle(request);
        }

        throw new Exception("No handler satisfied to handle the request");
    }
}