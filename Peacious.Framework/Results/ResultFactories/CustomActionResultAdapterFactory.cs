using System.Collections.Concurrent;
using Peacious.Framework.Results.Enums;
using Peacious.Framework.Results.ResultAdapters;

namespace Peacious.Framework.Results.ResultFactories;

public class CustomActionResultAdapterFactory : IActionResultAdapterFactory
{
    private static readonly object _lockObject = new();
    private static IActionResultAdapterFactory? _instance;

    public static IActionResultAdapterFactory Instance
    {
        get
        {
            if (_instance is not null)
            {
                return _instance;
            }
            lock (_lockObject)
            {
                _instance ??= new CustomActionResultAdapterFactory();
            }
            return _instance;
        }
    }

    private readonly ConcurrentDictionary<ResponseType, IActionResultAdapter> _adaptersCacheDictionary;

    private CustomActionResultAdapterFactory()
    {
        _adaptersCacheDictionary = new();
    }

    public IActionResultAdapter GetActionResultAdapter(ResponseType responseType)
    {

        if (_adaptersCacheDictionary.TryGetValue(responseType, out IActionResultAdapter? adapter))
        {
            return adapter;
        }

        if (adapter is not null)
        {
            return adapter;
        }

        lock (_lockObject)
        {
            if (_adaptersCacheDictionary.TryGetValue(responseType, out adapter))
            {
                return adapter;
            }

            if (adapter is not null)
            {
                return adapter;
            }

            adapter = GetNewInstance(responseType);

            _adaptersCacheDictionary.TryAdd(responseType, adapter);
        }

        return adapter;
    }

    private static IActionResultAdapter GetNewInstance(ResponseType responseType)
    {
        return responseType switch
        {
            ResponseType.Default => new DefaultActionResultAdapter(),
            _ => new DefaultActionResultAdapter(),
        }; ;
    }
}