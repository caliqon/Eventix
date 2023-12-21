namespace Eventix.State.Contracts;

public interface IStateEntity
{
    string Id { get; }
    string PartitionKey { get; }
    
    IDictionary<string, object> Data { get; }
    
    DateTimeOffset CreatedAt { get; }
    DateTimeOffset UpdatedAt { get; }
    DateTimeOffset? TimeToLive { get; }
}