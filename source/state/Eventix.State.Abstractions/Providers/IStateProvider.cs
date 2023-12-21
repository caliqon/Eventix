using Eventix.State.Contracts;
using FluentResults;

namespace Eventix.State.Providers;

public interface IStateProvider
{
    string Name { get; }
    
    Task<Result<IStateEntity>> GetAsync(string storeName, string id, CancellationToken cancellationToken = default);
    
    Task<Result<IStateEntity>> CreateAsync(IStateEntity entity, CancellationToken cancellationToken = default);
    
    Task<Result<IStateEntity>> UpdateAsync(IStateEntity entity, CancellationToken cancellationToken = default);
    
    Task<Result> DeleteAsync(string storeName, string id, CancellationToken cancellationToken = default);
}