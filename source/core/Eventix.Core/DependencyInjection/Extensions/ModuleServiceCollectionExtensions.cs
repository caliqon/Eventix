using Eventix.DependencyInjection.Contracts;
using Eventix.DependencyInjection.Models;
using Eventix.DependencyInjection.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Eventix.DependencyInjection.Extensions;

public static class ModuleServiceCollectionExtensions
{
    private static readonly Dictionary<string, IHostModule> Modules = new();

    public static WebApplicationBuilder ConfigureModules(this WebApplicationBuilder builder, ILogger logger,
        Action<IAppHostBuilder> configureDelegate)
    {
        var appHostBuilder = new DefaultAppHostBuilder(builder, logger);

        configureDelegate(appHostBuilder);
        
        return builder;
    }

    public static IAppHostBuilder AddModule<TModule>(this IAppHostBuilder builder)
        where TModule : IHostModule, new()
    {
        var module = Activator.CreateInstance<TModule>();

        var result = module.Register(builder);

        if (result.IsFailed)
            throw new Exception($"Failed to register module {module.Name}: {result.Errors.First().Message}");

        Modules.Add(module.Name, module);

        return builder;
    }

    public static IAppHostBuilder AddModule<TModule>(this IAppHostBuilder services, TModule module)
        where TModule : IHostModule
    {
        var result = module.Register(services);

        if (result.IsFailed)
            throw new Exception($"Failed to register module {module.Name}: {result.Errors.First().Message}");

        return services;
    }

    private static bool IsModuleRegistered<TModule>(Type type)
        where TModule : IHostModule
    {
        return Modules.Any(x => x.Value.GetType() == type);
    }

    public static bool IsModuleRegistered<TModule>(this IAppHostBuilder builder)
        where TModule : IHostModule
        => IsModuleRegistered<TModule>(typeof(TModule));

    public static bool IsModuleRegistered<TModule>(this IHostApplicationBuilder _)
        where TModule : IHostModule
        => IsModuleRegistered<TModule>(typeof(TModule));

    public static bool IsModuleRegistered<TModule>(this WebApplication _)
        where TModule : IHostModule
        => IsModuleRegistered<TModule>(typeof(TModule));

    public static bool IsModuleRegistered<TModule>(this IServiceProvider _)
        where TModule : IHostModule
        => IsModuleRegistered<TModule>(typeof(TModule));
}