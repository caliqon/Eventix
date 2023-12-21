using System.Text.Json;
using Eventix.DependencyInjection.Attributes;
using Eventix.DependencyInjection.Modules;
using Eventix.State.Providers.CosmosDb.Factories;
using Eventix.State.Providers.CosmosDb.Options;
using Eventix.State.Providers.CosmosDb.Serializers;
using Eventix.State.Providers.CosmosDb.Services;
using FluentResults;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Eventix.State.Providers.CosmosDb;

[ModuleName(ModuleName)]
public class CosmosDbStateHostModule() : HostModule(ModuleName)
{
    private const string ModuleName = "CosmosDbState";

    protected override Result Register()
    {
        ConfigureOptions<CosmosDbStateConfiguration>("Connection", postConfigure: configuration =>
        {
            configuration.ClientOptions.SerializerOptions = null;
            configuration.ClientOptions.Serializer = new CosmosDbJsonSerializer();
        });

        Services.AddSingleton<ICosmosDbConnectionFactory, CosmosDbConnectionFactory>();
        Services.AddHostedService<CosmosDbInitialisationService>();

        Services.AddKeyedScoped<IStateProvider, CosmosDbStateProvider>(CosmosDbStateProvider.ProviderName);

        return Result.Ok();
    }
}