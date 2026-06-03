using Lego.Core.Models;

namespace Lego.Core.Services;

/// <summary>
/// Query operations over the in-memory LEGO theme and set data.
/// </summary>
public interface ILegoService
{
    /// <summary>Finds a single set by its exact set number, or <c>null</c> if none matches.</summary>
    Set? FindSetByNumber(string setNumber);

    /// <summary>Finds sets whose name contains <paramref name="name"/> (case-insensitive).</summary>
    IReadOnlyList<Set> FindSetsByName(string name);

    /// <summary>Finds themes whose name contains <paramref name="name"/> (case-insensitive).</summary>
    IReadOnlyList<Theme> FindThemesByName(string name);

    /// <summary>Gets all themes.</summary>
    IReadOnlyList<Theme> GetAllThemes();

    /// <summary>Gets all sets belonging to the given theme.</summary>
    IReadOnlyList<Set> GetSetsByThemeId(int themeId);

    /// <summary>Gets a theme by its id.</summary>
    /// <exception cref="KeyNotFoundException">Thrown when no theme matches <paramref name="themeId"/>.</exception>
    Theme GetThemeById(int themeId);

    /// <summary>Gets a set by its set number.</summary>
    /// <exception cref="KeyNotFoundException">Thrown when no set matches <paramref name="setNumber"/>.</exception>
    Set GetSetById(string setNumber);

    /// <summary>Gets the immediate child themes of the given parent theme id.</summary>
    IReadOnlyList<Theme> GetThemesByParentId(int parentId);
}