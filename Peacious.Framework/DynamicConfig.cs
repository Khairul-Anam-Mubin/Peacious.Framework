﻿using System.Collections.Concurrent;
using Peacious.Framework.Extensions;

namespace Peacious.Framework;

public sealed class DynamicConfig
{
    private static readonly object LockObj = new();
    private static DynamicConfig? _instance;

    public static DynamicConfig Instance
    {
        get
        {
            if (_instance is not null) return _instance;
            lock (LockObj)
            {
                _instance ??= new DynamicConfig();
            }
            return _instance;
        }
    }

    private readonly ConcurrentDictionary<string, object> _dynamicConfig;

    private DynamicConfig()
    {
        _dynamicConfig = new ConcurrentDictionary<string, object>();
    }

    public void AddConfigFromPath(string configPath)
    {
        if (string.IsNullOrEmpty(configPath))
        {
            Console.WriteLine("Config Path Not Found");
            return;
        }

        try
        {
            var configText = File.ReadAllText(configPath);

            AddConfigFromText(configText);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void AddConfigFromText(string configText)
    {
        if (string.IsNullOrEmpty(configText))
        {
            Console.WriteLine("File is empty");
            return;
        }

        try
        {
            var configDictionary = configText.Deserialize<Dictionary<string, object>>();

            AddConfigFromDictionary(configDictionary);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void AddConfigFromDictionary(Dictionary<string, object>? configDictionary)
    {
        if (configDictionary == null)
        {
            Console.WriteLine("ConfigDictionary is null");
            return;
        }

        foreach (var kv in configDictionary)
        {
            AddConfig(kv.Key, kv.Value);
        }
    }

    public void AddConfig(string key, object value)
    {
        if (string.IsNullOrEmpty(key)) return;

        _dynamicConfig.TryAdd(key, value);
    }

    public T? GetConfig<T>(string key)
    {
        if (string.IsNullOrEmpty(key)) return default;

        _dynamicConfig.TryGetValue(key, out var value);

        return value.SmartCast<T>();
    }

    public T TryGetConfig<T>(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new Exception("Key is null here");
        }

        var value = GetConfig<T>(key);

        if (value == null)
        {
            throw new Exception("Value is null");
        }

        return value;
    }
}