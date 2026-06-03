namespace Lego.Core.Models;

/// <summary>
/// A LEGO theme as parsed from <c>themes.csv</c>.
/// </summary>
public sealed record Theme
{
    /// <summary>Unique theme identifier.</summary>
    public required int Id { get; init; }

    /// <summary>Display name of the theme.</summary>
    public required string Name { get; init; }

    /// <summary>Identifier of the parent theme, or <c>null</c> for a root theme.</summary>
    public int? ParentId { get; init; }
}