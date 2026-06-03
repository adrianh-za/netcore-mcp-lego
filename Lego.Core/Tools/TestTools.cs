using ModelContextProtocol.Server;

namespace Lego.Core.Tools;

[McpServerToolType]
public class TestTools
{
    public TestTools()
    {

    }

    [McpServerTool]
    public string Echo(string message)
    {
        return message;
    }

    [McpServerTool]
    public string CurrentUtc()
    {
        return DateTime.UtcNow.ToString("o");
    }
}