using Eventix.State.Providers.CosmosDb.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Eventix.State.Providers.CosmosDb.Factories;

public interface ICosmosDbConnectionFactory
{
    CosmosClient Create();
}

public class CosmosDbConnectionFactory : ICosmosDbConnectionFactory
{
    private readonly IOptions<CosmosDbStateConfiguration> _configuration;

    public CosmosDbConnectionFactory(IOptions<CosmosDbStateConfiguration> configuration)
    {
        _configuration = configuration;
    }
    
    public CosmosClient Create()
    {
        var config = _configuration.Value;

        return new(config.AccountEndpoint, config.AccountKey, config.ClientOptions);
    }
}