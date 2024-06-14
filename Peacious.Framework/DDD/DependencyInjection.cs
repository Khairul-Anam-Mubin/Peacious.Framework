using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.EDD;
using Peacious.Framework.Mediators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Peacious.Framework.DDD;

public static class DependencyInjection
{
    public static IServiceCollection AddDDD(this IServiceCollection services)
    {
        services.AddTransient<IEventExecutor, EventExecutor>();
        return services;
    }
}
