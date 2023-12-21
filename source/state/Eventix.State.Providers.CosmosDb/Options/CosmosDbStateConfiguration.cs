using Microsoft.Azure.Cosmos;

namespace Eventix.State.Providers.CosmosDb.Options;

public class CosmosDbStateConfiguration
{
    public string DatabaseName { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
    public int Throughput { get; set; } = 400;

    public CosmosClientOptions ClientOptions { get; set; } = new();
    
    public string AccountEndpoint { get; set; } = string.Empty;
    public string AccountKey { get; set; }
}