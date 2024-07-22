using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.Results.Enums;
using Peacious.Framework.Results.ErrorAdapters;

namespace Peacious.Framework.Results.ErrorFactories;

public class ErrorActionResultAdapterFactory(IServiceProvider rootServiceProvider)
    : IErrorActionResultAdapterFactory
{

    private readonly IServiceProvider _rootServiceProvider = rootServiceProvider;

    public IErrorActionResultAdapter GetErrorActionResultAdapter(ErrorResponseType responseType)
    {
        return _rootServiceProvider.GetRequiredKeyedService<IErrorActionResultAdapter>(responseType);
    }
}
