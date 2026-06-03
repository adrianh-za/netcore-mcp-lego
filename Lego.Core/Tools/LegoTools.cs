using Lego.Core.Models;
using Lego.Core.Services;
using ModelContextProtocol.Server;

namespace Lego.Core.Tools;

[McpServerToolType]
public class LegoTools(ILegoService legoService)
{
    [McpServerTool]
    public Set? FindSetByNumber(string setNumber)
    {
        return legoService.FindSetByNumber(setNumber);
    }

    [McpServerTool]
    public IReadOnlyList<Set> FindSetsByName(string name)
    {
        return legoService.FindSetsByName(name);
    }

    [McpServerTool]
    public IReadOnlyList<Theme> FindThemesByName(string name)
    {
        return legoService.FindThemesByName(name);
    }

    [McpServerTool]
    public IReadOnlyList<Theme> GetAllThemes()
    {
        return legoService.GetAllThemes();
    }

    [McpServerTool]
    public IReadOnlyList<Set> GetSetsByThemeId(int themeId)
    {
        return legoService.GetSetsByThemeId(themeId);
    }

    [McpServerTool]
    public Theme GetThemeById(int themeId)
    {
        return legoService.GetThemeById(themeId);
    }

    [McpServerTool]
    public Set GetSetById(string setNumber)
    {
        return legoService.GetSetById(setNumber);
    }

    [McpServerTool]
    public IReadOnlyList<Theme> GetThemesByParentId(int parentId)
    {
        return legoService.GetThemesByParentId(parentId);
    }
}