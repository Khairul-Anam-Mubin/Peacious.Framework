namespace Peacious.Framework.ORM.Interfaces;

public interface ISortComposer<out T>
{
    T Compose(ISort sort);
}