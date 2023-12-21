using Eventix.DependencyInjection.Contracts;
using FluentResults;

namespace Eventix.DependencyInjection.Modules;

public abstract class HostModule(string name) : IHostModule
{
    public string Name { get; } = name;
    public abstract Result Register(IAppHostBuilder builder);
}