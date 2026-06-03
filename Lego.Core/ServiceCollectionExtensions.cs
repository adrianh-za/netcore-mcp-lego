using Lego.Core.Data;
using Lego.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lego.Core;

/// <summary>
/// DI registration helpers for the LEGO data layer.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Binds <see cref="LegoDataOptions"/> from configuration and registers the
    /// <see cref="ILegoDataStore"/> (built once from the CSV files) as a singleton.
    /// </summary>
    public static IServiceCollection AddLego(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<LegoDataOptions>()
            .Bind(configuration.GetSection(LegoDataOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<LegoDataLoader>();
        services.AddSingleton<ILegoDataStore>(sp => sp.GetRequiredService<LegoDataLoader>().Load());
        services.AddScoped<ILegoService, LegoService>();
        return services;
    }
}