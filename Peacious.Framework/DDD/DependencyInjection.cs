using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.EDD;

namespace Peacious.Framework.DDD;

public static class DependencyInjection
{
    public static IServiceCollection AddDDD(this IServiceCollection services)
    {
        services.AddTransient<IEventExecutor, EventExecutor>();
        return services;
    }
}
