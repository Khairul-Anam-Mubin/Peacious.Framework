using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.Results.Enums;
using Peacious.Framework.Results.ResultAdapters;

namespace Peacious.Framework.Results.ResultFactories;

public class ActionResultAdapterFactory(IServiceProvider rootServiceProvider) : IActionResultAdapterFactory
{
    private readonly IServiceProvider _rootServiceProvider = rootServiceProvider;

    public IActionResultAdapter GetActionResultAdapter(ResponseType responseType)
    {
        return _rootServiceProvider.GetRequiredKeyedService<IActionResultAdapter>(responseType);
    }
}
