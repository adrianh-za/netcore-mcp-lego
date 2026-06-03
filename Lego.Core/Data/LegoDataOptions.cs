using System.ComponentModel.DataAnnotations;

namespace Lego.Core.Data;

/// <summary>
/// Strongly typed configuration for locating the LEGO CSV data files.
/// Bound from the <c>"LegoData"</c> section of configuration via <see cref="Microsoft.Extensions.Options.IOptions{T}"/>.
/// </summary>
public sealed class LegoDataOptions
{
    /// <summary>Configuration section name this options class binds to.</summary>
    public const string SectionName = "LegoData";

    /// <summary>Path to <c>themes.csv</c>. May be absolute or relative to the content root.</summary>
    [Required]
    public string ThemesFilePath { get; set; } = string.Empty;

    /// <summary>Path to <c>sets.csv</c>. May be absolute or relative to the content root.</summary>
    [Required]
    public string SetsFilePath { get; set; } = string.Empty;
}