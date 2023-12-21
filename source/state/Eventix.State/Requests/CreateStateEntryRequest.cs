namespace Eventix.State.Requests;

public class CreateStateEntryRequest
{
    public string Key { get; set; }
    public IDictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
    public DateTimeOffset? ExpiresAt { get; set; }
}