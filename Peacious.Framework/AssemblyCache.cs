using System.Collections.Concurrent;
using System.Reflection;

namespace Peacious.Framework;

public sealed class AssemblyCache
{
    private static readonly object LockObj = new();
    private static AssemblyCache? _instance;

    public static AssemblyCache Instance
    {
        get
        {
            if (_instance is not null)
            {
                return _instance;
            }
            lock (LockObj)
            {
                _instance ??= new AssemblyCache();
            }
            return _instance;
        }
    }

    private readonly ConcurrentDictionary<string, Assembly> _assemblyDictionary;

    private AssemblyCache()
    {
        _assemblyDictionary = new();
    }

    public AssemblyCache AddAllAssembliesByAssemblyPrefix(string assemblyPrefix)
    {
        var entryAssemblyLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

        if (!string.IsNullOrEmpty(entryAssemblyLocation))
        {
            AddAllAssemblies(entryAssemblyLocation, assemblyPrefix);
        }

        var executingAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!string.IsNullOrEmpty(executingAssemblyLocation))
        {
            AddAllAssemblies(executingAssemblyLocation, assemblyPrefix);
        }

        return this;
    }

    public AssemblyCache AddAllAssemblies(string location, string assemblyPrefix)
    {
        if (string.IsNullOrEmpty(location)) return this;

        var files = Directory.GetFiles(location);

        foreach (var file in files)
        {
            try
            {
                var fileInfo = new FileInfo(file);

                if (!fileInfo.Name.StartsWith(assemblyPrefix, StringComparison.InvariantCultureIgnoreCase) ||
                    fileInfo.Extension != ".dll") continue;

                var assemblyName = Path.GetFileNameWithoutExtension(file);
                if (string.IsNullOrEmpty(assemblyName)) continue;

                AddAssembliesByAssemblyNames(assemblyName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        return this;
    }

    public AssemblyCache AddAssembliesByAssemblyNames(params string[] assemblyNames)
    {
        foreach (var assemblyName in assemblyNames)
        {
            try
            {
                var assembly = Assembly.Load(assemblyName);

                return AddAssembly(assembly);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return this;
    }

    public AssemblyCache AddAssemblies(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            AddAssembly(assembly);
        }

        return this;
    }

    public AssemblyCache AddAssembly(Assembly assembly)
    {
        if (string.IsNullOrEmpty(assembly.FullName) ||
            _assemblyDictionary.ContainsKey(assembly.FullName))
        {
            Console.WriteLine($"{assembly.FullName} already added\n");
            return this;
        }

        _assemblyDictionary.TryAdd(assembly.FullName, assembly);
        Console.WriteLine($"Added Assembly {assembly.FullName}\n");

        return this;
    }

    public Assembly[] GetAddedAssemblies()
    {
        return _assemblyDictionary.Values.ToArray();
    }
}