using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Peacious.Framework.Extensions;

namespace Peacious.Framework.ServiceInstaller;

public static class DependencyInjection
{
    public static IServiceCollection InstallServices(this IServiceCollection services,
        IConfiguration configuration, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetExportedTypes())
            {
                if (type.CanInstantiate() == false || type.IsAssignableTo(typeof(IServiceInstaller)) == false)
                {
                    continue;
                }

                var installer = Activator.CreateInstance(type).SmartCast<IServiceInstaller>();

                installer?.Install(services, configuration);
            }
        }

        return services;
    }
}