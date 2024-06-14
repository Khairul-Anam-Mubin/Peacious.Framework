using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Peacious.Framework.Extensions;
using Peacious.Framework.ServiceInstaller;
using System.Reflection;

namespace Peacious.Framework.Extensions;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder AddCustomConfigurationJsonFile(this WebApplicationBuilder builder, string jsonFilePath)
    {
        if (string.IsNullOrEmpty(jsonFilePath))
        {
            throw new Exception("Json File Path can not be null or empty.");
        }

        builder.Configuration.AddJsonFile(jsonFilePath, false, true);

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