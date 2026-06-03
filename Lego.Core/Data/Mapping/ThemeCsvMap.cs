using CsvHelper.Configuration;
using Lego.Core.Models;

namespace Lego.Core.Data.Mapping;

/// <summary>
/// Maps the columns of <c>themes.csv</c> onto the <see cref="Theme"/> record.
/// </summary>
public sealed class ThemeCsvMap : ClassMap<Theme>
{
    public ThemeCsvMap()
    {
        Map(t => t.Id).Name("id");
        Map(t => t.Name).Name("name");
        Map(t => t.ParentId).Name("parent_id");
    }
}