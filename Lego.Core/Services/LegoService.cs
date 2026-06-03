using Lego.Core.Data;
using Lego.Core.Models;

namespace Lego.Core.Services;

/// <summary>
/// Default <see cref="ILegoService"/> backed by the in-memory <see cref="ILegoDataStore"/>.
/// </summary>
public sealed class LegoService(ILegoDataStore store) : ILegoService
{
    public Set? FindSetByNumber(string setNumber) =>
        store.Sets.FirstOrDefault(s => string.Equals(s.SetNum, setNumber, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<Set> FindSetsByName(string name) =>
        store.Sets
            .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

    public IReadOnlyList<Theme> FindThemesByName(string name) =>
        store.Themes
            .Where(t => t.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

    public IReadOnlyList<Theme> GetAllThemes() =>
        store.Themes;

    public IReadOnlyList<Set> GetSetsByThemeId(int themeId) =>
        store.Sets
            .Where(s => s.ThemeId == themeId)
            .ToList();

    public Theme GetThemeById(int themeId) =>
        store.Themes.FirstOrDefault(t => t.Id == themeId)
        ?? throw new KeyNotFoundException($"Theme '{themeId}' was not found.");

    public Set GetSetById(string setNumber) =>
        FindSetByNumber(setNumber)
        ?? throw new KeyNotFoundException($"Set '{setNumber}' was not found.");

    public IReadOnlyList<Theme> GetThemesByParentId(int parentId) =>
        store.Themes
            .Where(t => t.ParentId == parentId)
            .ToList();
}