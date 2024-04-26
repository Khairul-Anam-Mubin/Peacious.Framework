using System.Text;
using KCluster.Framework.ORM.Enums;
using KCluster.Framework.ORM.Interfaces;

namespace KCluster.Framework.ORM.Sql.Composers;

public class SqlDbSortComposer : ISortComposer<string>
{
    public string Compose(ISort sort)
    {
        if (!sort.SortFields.Any())
        {
            return string.Empty;
        }

        var builder = new StringBuilder();

        for (var i = 0; i + 1 < sort.SortFields.Count; i++)
        {
            switch (sort.SortFields[i].SortDirection)
            {
                case SortDirection.Ascending:
                    builder.Append($"{sort.SortFields[i].FieldKey} ASC, ");
                    break;
                case SortDirection.Descending:
                    builder.Append($"{sort.SortFields[i].FieldKey} DESC, ");
                    break;
            }
        }

        switch (sort.SortFields.Last().SortDirection)
        {
            case SortDirection.Ascending:
                builder.Append($"{sort.SortFields.Last().FieldKey} ASC");
                break;
            case SortDirection.Descending:
                builder.Append($"{sort.SortFields.Last().FieldKey} DSC");
                break;
        }

        return builder.ToString();
    }

}