using Lego.Core;
using Lego.Core.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
 * Pin content root to the app's output folder so config/data resolve correctly
 * even when launched from a different working directory (e.g., repo root).
 *
 * Now you can run
 *
 * First terminal: dotnet run --project Lego.Mcp.Stream
 * Second terminl: npx @modelcontextprotocol/inspector
 *
 * from the repo root and the MCP server will find the config/data files without needing to cd into the Lego.Mcp.Stream folder first.
 */
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory
});

//Wire up the MCP server and tools
builder.Services.AddLego(builder.Configuration);
builder.Services.AddScoped<TestTools>();
builder.Services.AddScoped<LegoTools>();
builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<TestTools>()
    .WithTools<LegoTools>();

//Run the server
var app = builder.Build();
var configuration = app.Services.GetRequiredService<IConfiguration>();
var port = configuration.GetValue("McpServer:Port", 5250);
app.Urls.Add($"http://+:{port}");
app.MapMcp("/mcp");
app.Run();