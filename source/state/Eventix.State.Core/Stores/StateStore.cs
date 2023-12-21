using Eventix.State.Contracts;
using Eventix.State.Factories;
using FluentResults;

namespace Eventix.State.Stores;

public class StateStore : IStateStore
{
    private readonly IStateProviderFactory _stateProviderFactory;

    public StateStore(IStateProviderFactory stateProviderFactory)
    {
        _stateProviderFactory = stateProviderFactory;
    }
    
    public async ValueTask<Result<IStateEntity>> GetAsync(string storeName, string id, string providerName, CancellationToken cancellationToken = default)
    {
        return await _stateProviderFactory.GetProvider(providerName).GetAsync(storeName, id, cancellationToken);
    }

    public async ValueTask<Result<IStateEntity>> CreateAsync(string storeName, string providerName, IStateEntity entity,
        CancellationToken cancellationToken = default)
    {
        return await _stateProviderFactory.GetProvider(providerName).CreateAsync(entity, cancellationToken);
    }

    public async ValueTask<Result<IStateEntity>> UpdateAsync(string storeName, string providerName, IStateEntity entity,
        CancellationToken cancellationToken = default)
    {
        return await _stateProviderFactory.GetProvider(providerName).UpdateAsync(entity, cancellationToken);
    }

    public async ValueTask<Result> DeleteAsync(string storeName, string id, string providerName, CancellationToken cancellationToken = default)
    {
        return await _stateProviderFactory.GetProvider(providerName).DeleteAsync(storeName, id, cancellationToken);
    }
}