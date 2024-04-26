using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KCluster.Framework.ServiceInstaller;

public interface IServiceInstaller
{
    void Install(IServiceCollection services, IConfiguration configuration);
}