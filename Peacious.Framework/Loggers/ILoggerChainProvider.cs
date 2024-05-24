namespace Peacious.Framework.Loggers;

public interface ILoggerChainProvider
{
    ALogger GetLoggerChain();
}