namespace KCluster.Framework.Loggers;

public interface ILoggerChainProvider
{
    ALogger GetLoggerChain();
}