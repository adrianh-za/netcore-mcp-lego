using Lego.Core.Models;
using Lego.Core.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Lego.Api.Endpoints;

/// <summary>
/// Minimal API endpoints for browsing the LEGO theme and set data.
/// </summary>
public static class LegoEndpoints
{
    /// <summary>Maps the LEGO endpoints under the <c>/api/lego</c> route group.</summary>
    public static IEndpointRouteBuilder MapLegoEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/api/lego")
            .AllowAnonymous()
            .WithTags("Lego");

        group.MapGet("/sets", FindSetsByName)
            .WithName(nameof(FindSetsByName))
            .WithSummary("Search sets by partial name (case-insensitive).")
            .Produces<IReadOnlyList<Set>>();

        group.MapGet("/sets/{setNumber}", GetSetByNumber)
            .WithName(nameof(GetSetByNumber))
            .WithSummary("Get a single set by its set number.")
            .Produces<Set>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/themes", FindThemesByName)
            .WithName(nameof(FindThemesByName))
            .WithSummary("Search themes by partial name (case-insensitive).")
            .Produces<IReadOnlyList<Theme>>();

        group.MapGet("/themes/all", GetAllThemes)
            .WithName(nameof(GetAllThemes))
            .WithSummary("Get all themes.")
            .Produces<IReadOnlyList<Theme>>();

        group.MapGet("/themes/{themeId:int}", GetThemeById)
            .WithName(nameof(GetThemeById))
            .WithSummary("Get a single theme by its id.")
            .Produces<Theme>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/themes/{themeId:int}/sets", GetSetsByThemeId)
            .WithName(nameof(GetSetsByThemeId))
            .WithSummary("Get all sets belonging to a theme.")
            .Produces<IReadOnlyList<Set>>();

        group.MapGet("/themes/{parentId:int}/children", GetThemesByParentId)
            .WithName(nameof(GetThemesByParentId))
            .WithSummary("Get the immediate child themes of a parent theme.")
            .Produces<IReadOnlyList<Theme>>();

        return builder;
    }

    private static Ok<IReadOnlyList<Set>> FindSetsByName(string name, ILegoService service)
        => TypedResults.Ok(service.FindSetsByName(name));

    private static Results<Ok<Set>, NotFound> GetSetByNumber(string setNumber, ILegoService service)
    {
        try
        {
            return TypedResults.Ok(service.GetSetById(setNumber));
        }
        catch (KeyNotFoundException)
        {
            return TypedResults.NotFound();
        }
    }

    private static Ok<IReadOnlyList<Theme>> FindThemesByName(string name, ILegoService service)
        => TypedResults.Ok(service.FindThemesByName(name));

    private static Ok<IReadOnlyList<Theme>> GetAllThemes(ILegoService service)
        => TypedResults.Ok(service.GetAllThemes());

    private static Results<Ok<Theme>, NotFound> GetThemeById(int themeId, ILegoService service)
    {
        try
        {
            return TypedResults.Ok(service.GetThemeById(themeId));
        }
        catch (KeyNotFoundException)
        {
            return TypedResults.NotFound();
        }
    }

    private static Ok<IReadOnlyList<Set>> GetSetsByThemeId(int themeId, ILegoService service)
        => TypedResults.Ok(service.GetSetsByThemeId(themeId));

    private static Ok<IReadOnlyList<Theme>> GetThemesByParentId(int parentId, ILegoService service)
        => TypedResults.Ok(service.GetThemesByParentId(parentId));
}