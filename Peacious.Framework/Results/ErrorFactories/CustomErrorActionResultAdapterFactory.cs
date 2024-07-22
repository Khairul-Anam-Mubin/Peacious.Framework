using System.Collections.Concurrent;
using Peacious.Framework.Results.Enums;
using Peacious.Framework.Results.ErrorAdapters;

namespace Peacious.Framework.Results.ErrorFactories;

public class CustomErrorActionResultAdapterFactory : IErrorActionResultAdapterFactory
{
    private static readonly object _lockObject = new();
    private static IErrorActionResultAdapterFactory? _instance;

    public static IErrorActionResultAdapterFactory Instance
    {
        get
        {
            if (_instance is not null)
            {
                return _instance;
            }
            lock (_lockObject)
            {
                _instance ??= new CustomErrorActionResultAdapterFactory();
            }
            return _instance;
        }
    }

    private readonly ConcurrentDictionary<ErrorResponseType, IErrorActionResultAdapter> _adaptersCacheDictionary;

    private CustomErrorActionResultAdapterFactory()
    {
        _adaptersCacheDictionary = new();
    }

    public IErrorActionResultAdapter GetErrorActionResultAdapter(ErrorResponseType responseType)
    {
        IErrorActionResultAdapter? adapter;

        if (_adaptersCacheDictionary.TryGetValue(responseType, out adapter))
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

    private IErrorActionResultAdapter GetNewInstance(ErrorResponseType responseType)
    {
        return responseType switch
        {
            ErrorResponseType.ProblemDetails => new ProblemDetailsErrorActionResultAdapter(),
            _ => new DefaultErrorActionResultAdapter(),
        };
    }
}