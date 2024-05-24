using Peacious.Framework.ORM.Enums;

namespace Peacious.Framework.ORM.Interfaces;

public interface IFilter
{
    List<ISimpleFilter> SimpleFilters { get; set; }
    List<IFilter> CompoundFilters { get; set; }
    CompoundLogic? Logic { get; set; }
}