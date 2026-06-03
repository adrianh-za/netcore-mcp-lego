using Lego.Core.Models;

namespace Lego.Core.Data;

/// <summary>
/// Immutable implementation of <see cref="ILegoDataStore"/>. Instances are built once at
/// startup by <see cref="LegoDataLoader"/> and shared as a singleton.
/// </summary>
public sealed class LegoDataStore(
    IReadOnlyList<Theme> themes,
    IReadOnlyList<Set> sets)
    : ILegoDataStore
{
    public IReadOnlyList<Theme> Themes { get; } = themes;

    public IReadOnlyList<Set> Sets { get; } = sets;
}