using Eventix.State.Contracts;
using FluentResults;

namespace Eventix.State.Stores;

public interface IStateStore
{
    ValueTask<Result<IStateEntity>> GetAsync(string storeName, string id, string providerName, CancellationToken cancellationToken = default);
    
    ValueTask<Result<IStateEntity>> CreateAsync(string storeName, string providerName, IStateEntity entity, CancellationToken cancellationToken = default);
    
    ValueTask<Result<IStateEntity>> UpdateAsync(string storeName, string providerName, IStateEntity entity, CancellationToken cancellationToken = default);
    
    ValueTask<Result> DeleteAsync(string storeName, string id, string providerName, CancellationToken cancellationToken = default);
}