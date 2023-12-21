using Eventix.State.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Eventix.State.Factories;

public class StateProviderFactory : IStateProviderFactory
{
    private readonly IServiceProvider _services;

    public StateProviderFactory(IServiceProvider services)
    {
        _services = services;
    }
    
    public IStateProvider GetProvider(string name)
    {
        return _services.GetRequiredKeyedService(typeof(IStateProvider), name) as IStateProvider
               ?? throw new Exception($"Failed to get state provider {name}");
    }
}