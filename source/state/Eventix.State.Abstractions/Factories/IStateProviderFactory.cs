using Eventix.State.Providers;

namespace Eventix.State.Factories;

public interface IStateProviderFactory
{
    IStateProvider GetProvider(string name);
}