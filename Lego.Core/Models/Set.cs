namespace Lego.Core.Models;

/// <summary>
/// A LEGO set as parsed from <c>sets.csv</c>.
/// </summary>
public sealed record Set
{
    /// <summary>Unique set number (e.g. <c>"75192-1"</c>).</summary>
    public required string SetNum { get; init; }

    /// <summary>Display name of the set.</summary>
    public required string Name { get; init; }

    /// <summary>Year the set was released.</summary>
    public required int Year { get; init; }

    /// <summary>Identifier of the <see cref="Theme"/> this set belongs to.</summary>
    public required int ThemeId { get; init; }

    /// <summary>Number of parts in the set.</summary>
    public required int NumParts { get; init; }

    /// <summary>URL of the set's image.</summary>
    public required string ImgUrl { get; init; }
}