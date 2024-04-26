using KCluster.Framework.ORM.Enums;

namespace KCluster.Framework.ORM.Interfaces;

public interface IFilter
{
    List<ISimpleFilter> SimpleFilters { get; set; }
    List<IFilter> CompoundFilters { get; set; }
    CompoundLogic? Logic { get; set; }
}