using CsvHelper.Configuration;
using Lego.Core.Models;

namespace Lego.Core.Data.Mapping;

/// <summary>
/// Maps the columns of <c>sets.csv</c> onto the <see cref="Set"/> record.
/// </summary>
public sealed class SetCsvMap : ClassMap<Set>
{
    public SetCsvMap()
    {
        Map(s => s.SetNum).Name("set_num");
        Map(s => s.Name).Name("name");
        Map(s => s.Year).Name("year");
        Map(s => s.ThemeId).Name("theme_id");
        Map(s => s.NumParts).Name("num_parts");
        Map(s => s.ImgUrl).Name("img_url");
    }
}