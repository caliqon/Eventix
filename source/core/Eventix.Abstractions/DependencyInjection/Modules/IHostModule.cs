using Eventix.DependencyInjection.Contracts;
using FluentResults;

namespace Eventix.DependencyInjection.Modules;

public interface IHostModule
{
    string Name { get; }
    
    public Result Register(IAppHostBuilder builder);
}