using Eventix.State.Providers.CosmosDb.Factories;
using Eventix.State.Providers.CosmosDb.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Eventix.State.Providers.CosmosDb.Services;

public class CosmosDbInitialisationService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public CosmosDbInitialisationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        
        var factory = scope.ServiceProvider.GetRequiredService<ICosmosDbConnectionFactory>();
        var configuration = scope.ServiceProvider.GetRequiredService<IOptions<CosmosDbStateConfiguration>>().Value;
        
        var client = factory.Create();
        
        var database = await client.CreateDatabaseIfNotExistsAsync(configuration.DatabaseName, configuration.Throughput, cancellationToken: cancellationToken);
        await database.Database.CreateContainerIfNotExistsAsync(configuration.ContainerName, "/partitionKey", cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}