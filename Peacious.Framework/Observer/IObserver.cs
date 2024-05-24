namespace Peacious.Framework.Observer;

public interface IObserver<in T>
{
    Task ObserveAsync(T data);
}