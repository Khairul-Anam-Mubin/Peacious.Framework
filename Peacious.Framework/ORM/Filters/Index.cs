using Peacious.Framework.ORM.Interfaces;

namespace Peacious.Framework.ORM.Filters;

public class Index : IIndex
{
    public List<IIndexKey> IndexKeys { get; set; }

    public Index()
    {
        IndexKeys = new List<IIndexKey>();
    }

    public IIndex Add(IIndexKey indexKey)
    {
        IndexKeys.Add(indexKey);
        return this;
    }
}