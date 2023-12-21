using Eventix.DependencyInjection.Modules;
using Eventix.State.Factories;
using Eventix.State.Stores;
using FluentResults;
using Microsoft.Extensions.DependencyInjection;

namespace Eventix.State;

public class StateHostModule() : HostModule(ModuleName)
{
    private const string ModuleName = "State";
    
    protected override Result Register()
    {
        Services.AddScoped<IStateProviderFactory, StateProviderFactory>();
        Services.AddScoped<IStateStore, StateStore>();
        
        return Result.Ok();
    }
}