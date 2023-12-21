using Eventix.State.Contracts;

namespace Eventix.State.Entities;

public class StateEntity : IStateEntity
{
    public StateEntity(string id, string partitionKey, IDictionary<string, object> data, DateTimeOffset createdAt, DateTimeOffset updatedAt, DateTimeOffset? timeToLive = null)
    {
        Id = id;
        PartitionKey = partitionKey;
        Data = data;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        TimeToLive = timeToLive;
    }
    
    public StateEntity(string id, string partitionKey, IDictionary<string, object> data, DateTimeOffset createdAt, DateTimeOffset? timeToLive = null)
    {
        Id = id;
        PartitionKey = partitionKey;
        Data = data;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
        TimeToLive = timeToLive;
    }
    
    public StateEntity(string id, string partitionKey, IDictionary<string, object> data, DateTimeOffset? timeToLive = null)
    {
        Id = id;
        PartitionKey = partitionKey;
        Data = data;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
        TimeToLive = timeToLive;
    }


    public StateEntity()
    {
        
    }
    
    public string Id { get; set; }
    public string PartitionKey { get; set; } 
    public IDictionary<string, object> Data { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? TimeToLive { get; set; }
}