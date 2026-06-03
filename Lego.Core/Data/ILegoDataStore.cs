using Lego.Core.Models;

namespace Lego.Core.Data;

/// <summary>
/// In-memory, read-only store of all LEGO themes and sets.
/// Registered as a singleton and consumed by controllers/services.
/// </summary>
public interface ILegoDataStore
{
    /// <summary>All themes, in the order they were read from the CSV.</summary>
    IReadOnlyList<Theme> Themes { get; }

    /// <summary>All sets, in the order they were read from the CSV.</summary>
    IReadOnlyList<Set> Sets { get; }
}