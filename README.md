# LEGO MCP Server

A .NET 10.0 solution that exposes LEGO set and theme data through a REST API and MCP (Model Context Protocol) servers. The data is sourced from official LEGO CSV datasets containing thousands of sets organized into hierarchical themes.

## Architecture

| Project | Type | Description |
|---------|------|-------------|
| `Lego.Core` | Class Library | Shared models (`Set`, `Theme`), services, and MCP tool implementations |
| `Lego.Api` | Web API | Standard REST API with Swagger UI at `/swagger` for interactive exploration |
| `Lego.Mcp.StdIo` | Console App | MCP server using stdio transport for direct process communication |
| `Lego.Mcp.Stream` | MCP Web Server | MCP server using HTTP transport for network-based access |

## Data Model

### Theme
A LEGO theme organizes sets into categories. Themes can have parent-child relationships.

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `int` | Unique theme identifier |
| `Name` | `string` | Display name (e.g., "Star Wars", "Technic") |
| `ParentId` | `int?` | Parent theme ID, or `null` for root themes |

### Set
A LEGO set represents a physical LEGO product.

| Property | Type | Description |
|----------|------|-------------|
| `SetNum` | `string` | Unique set number (e.g., "75192-1") |
| `Name` | `string` | Display name of the set |
| `Year` | `int` | Release year |
| `ThemeId` | `int` | ID of the theme this set belongs to |
| `NumParts` | `int` | Number of parts in the set |
| `ImgUrl` | `string` | URL to the set's image |

**Data Source:** CSV files (`Lego.Core/Data/sets.csv` and `Lego.Core/Data/themes.csv`) sourced from [Rebrickable](https://rebrickable.com/downloads/) are embedded as content and loaded at runtime.

## Prerequisites

- .NET 10.0 SDK
- Node.js (for running the MCP inspector)

## Usage

### 1. REST API (Lego.Api)

A standard REST API providing endpoints for browsing LEGO sets and themes. The API includes Swagger UI at the `/swagger` endpoint for interactive exploration.

#### Endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/api/lego/sets` | Search sets by name (query param: `name`) |
| GET | `/api/lego/sets/{setNumber}` | Get a specific set by its set number |
| GET | `/api/lego/themes` | Search themes by name (query param: `name`) |
| GET | `/api/lego/themes/all` | Get all themes |
| GET | `/api/lego/themes/{themeId}` | Get a specific theme by ID |
| GET | `/api/lego/themes/{themeId}/sets` | Get all sets belonging to a theme |
| GET | `/api/lego/themes/{parentId}/children` | Get immediate child themes of a parent theme |

#### Example Requests

```bash
# Search for Star Wars sets
curl "http://localhost:5000/api/lego/sets?name=star%20wars"

# Get a specific set
curl "http://localhost:5000/api/lego/sets/75192-1"

# Get all themes
curl "http://localhost:5000/api/lego/themes/all"

# Get sets in the Technic theme (ID 15)
curl "http://localhost:5000/api/lego/themes/15/sets"
```

#### Running
```bash
dotnet run --project Lego.Api
```

Swagger UI is available at `http://localhost:<port>/swagger` when running in development.

### 2. MCP Server - StdIo (Lego.Mcp.StdIo)

An MCP server using stdio transport. This allows direct process-to-process communication with MCP clients.

#### Running with MCP Inspector
```bash
# From solution root
npx @modelcontextprotocol/inspector dotnet run --project Lego.Mcp.StdIo
```

This launches the MCP inspector and automatically connects to the stdio server.

In the MCP Inspector:
* Transport Type: `STDIO`
* Command: `dotnet`
* Arguments: `run --project Lego.Mcp.StdIo`
* Authentication: No custom headers or tokens required.

#### Running Standalone
```bash
dotnet run --project Lego.Mcp.StdIo
```

The server reads from stdin and writes to stdout, making it compatible with any MCP client that supports stdio transport.

### 3. MCP Server - Stream/HTTP (Lego.Mcp.Stream)

An MCP server using HTTP transport. This runs as a web application and exposes MCP tools via a REST-like endpoint.

#### Running
```bash
dotnet run --project Lego.Mcp.Stream
```

The server starts on port `5250` by default.

#### Connecting with MCP Inspector
```bash
npx @modelcontextprotocol/inspector
```

In the MCP Inspector:
* Transport Type: `Streamable HTTP`
* URL: `http://localhost:5250/mcp`
* Connection Type: `Via Proxy`
* Authentication: No custom headers or tokens required.


#### Configuration
The port can be configured in `Lego.Mcp.Stream/appsettings.json`:
```json
{
  "McpServer": {
    "Port": 5250
  }
}
```

## MCP Tools

Both MCP servers (`StdIo` and `Stream`) expose the same set of tools.

### LegoTools

Tools for querying LEGO set and theme data.

| Tool | Parameters | Description |
|------|------------|-------------|
| `FindSetByNumber` | `setNumber: string` | Find a set by its exact set number (returns null if not found) |
| `FindSetsByName` | `name: string` | Search sets by name (case-insensitive partial match, returns empty list if none) |
| `FindThemesByName` | `name: string` | Search themes by name (case-insensitive partial match, returns empty list if none) |
| `GetAllThemes` | - | Get all themes |
| `GetSetsByThemeId` | `themeId: int` | Get all sets belonging to a specific theme |
| `GetThemeById` | `themeId: int` | Get a specific theme by its ID (throws if not found) |
| `GetSetById` | `setNumber: string` | Get a specific set by its set number (throws if not found) |
| `GetThemesByParentId` | `parentId: int` | Get all immediate child themes of a parent theme |

### TestTools

Utility tools for testing and debugging.

| Tool | Parameters | Description |
|------|------------|-------------|
| `Echo` | `message: string` | Echoes back the input message |
| `CurrentUtc` | - | Returns the current UTC timestamp in ISO 8601 format |

## Development

- **Target Framework:** .NET 10.0
- **Language:** C# 12 with nullable reference types enabled
- **Build:** Data CSV files are automatically copied to output directories

## Configuration

- `Lego.Mcp.Stream` port is configurable via `McpServer:Port` in `appsettings.json` (default: 5250)
- Both MCP servers log to stderr with `Trace` level for debugging