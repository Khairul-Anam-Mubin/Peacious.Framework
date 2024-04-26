namespace KCluster.Framework.ORM.Interfaces;

public interface ISortComposer<out T>
{
    T Compose(ISort sort);
}