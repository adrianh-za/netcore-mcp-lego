using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Lego.Core.Data.Mapping;
using Lego.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lego.Core.Data;

/// <summary>
/// Reads the themes and sets CSV files and builds the in-memory <see cref="ILegoDataStore"/>,
/// wiring themes to their parents/children and sets to their owning themes.
/// </summary>
public sealed class LegoDataLoader(IOptions<LegoDataOptions> options, ILogger<LegoDataLoader> logger)
{
    private readonly LegoDataOptions _options = options.Value;

    /// <summary>Reads both CSV files and builds the linked, in-memory data store.</summary>
    public ILegoDataStore Load()
    {
        var themesPath = ResolvePath(_options.ThemesFilePath);
        var setsPath = ResolvePath(_options.SetsFilePath);

        logger.LogInformation("Loading LEGO data from themes='{ThemesPath}', sets='{SetsPath}'.",
            themesPath, setsPath);

        var themes = ReadCsv<Theme, ThemeCsvMap>(themesPath);
        var sets = ReadCsv<Set, SetCsvMap>(setsPath);

        logger.LogInformation("Loaded {ThemeCount} themes and {SetCount} sets.",
            themes.Count, sets.Count);

        return new LegoDataStore(themes, sets);
    }

    private static string ResolvePath(string path) =>
        Path.IsPathRooted(path)
            ? path
            : Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, path));

    private static List<T> ReadCsv<T, TMap>(string path) where TMap : ClassMap<T>
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,   // first row is the column names
            MissingFieldFound = null, // tolerate absent optional fields (e.g. parent_id)
            TrimOptions = TrimOptions.Trim,
        };

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<TMap>();
        return csv.GetRecords<T>().ToList();
    }
}
