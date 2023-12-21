using System.Text.Json;
using System.Text.Json.Serialization;

namespace Eventix.State.Providers.CosmosDb.Entities;

public class CosmosDbEntity
{
    public string Id { get; set; }
    public string PartitionKey { get; set; }
    public IDictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? TimeToLive { get; set; }
    
    public int TTL { get; set; } = -1;
}