using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Peacious.Framework.Extensions;
using Peacious.Framework.ServiceInstaller;
using System.Reflection;

namespace Peacious.Framework.Extensions;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder AddGlobalConfig(this WebApplicationBuilder builder, string globalConfigPath)
    {
        if (string.IsNullOrEmpty(globalConfigPath))
        {
            throw new Exception("Global Config Path not found");
        }

        builder.Configuration.AddJsonFile(globalConfigPath, false, true);

        return builder;
    }

    public static WebApplicationBuilder AddAllAssembliesByAssemblyPrefix(this WebApplicationBuilder builder, string assemblyPrefix)
    {
        AssemblyCache.Instance.AddAllAssembliesByAssemblyPrefix(assemblyPrefix);

        return builder;
    }

    public static WebApplicationBuilder AddAssembliesByAssemblyNames(this WebApplicationBuilder builder, List<string> assemblyNames)
    {
        AssemblyCache.Instance.AddAssembliesByAssemblyNames(assemblyNames);

        return builder;
    }

    public static WebApplicationBuilder InstallServices(this WebApplicationBuilder builder, List<Assembly> assemblies)
    {
        builder.Services.InstallServices(builder.Configuration, assemblies);
        return builder;
    }

    public static WebApplicationBuilder InstallServices(this WebApplicationBuilder builder, params Assembly[] assemblies)
    {
        builder.Services.InstallServices(builder.Configuration, assemblies);
        return builder;
    }
}