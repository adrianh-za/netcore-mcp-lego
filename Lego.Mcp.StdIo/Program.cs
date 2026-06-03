using Lego.Core;
using Lego.Core.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/*
 * Pin content root to the app's output folder so config/data resolve correctly
 * even when launched from a different working directory (e.g., repo root).
 *
 * Now you can run
 *
 * npx @modelcontextprotocol/inspector dotnet run --project Lego.Mcp.StdIo/Lego.Mcp.StdIo.csproj
 *
 * from the repo root and the MCP server will find the config/data files without needing to cd into the Lego.Mcp.StdIo folder first.
 */
var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory
});

//Dump all the logs
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Trace;
});

//Wire up the MCP server and tools
builder.Services.AddLego(builder.Configuration);
builder.Services.AddScoped<TestTools>();
builder.Services.AddScoped<LegoTools>();
builder.Services.AddMcpServer()
    .WithStdioServerTransport() //NB: Order matters! Tools must be registered after the transport is registered.
    .WithTools<TestTools>()
    .WithTools<LegoTools>();

//Run the server
await builder.Build().RunAsync();