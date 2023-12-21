using System.Text.Json;
using Eventix.State.Attributes;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using Eventix.State.Contracts;
using Eventix.State.Entities;
using Eventix.State.Providers.CosmosDb.Entities;
using Eventix.State.Providers.CosmosDb.Factories;
using Eventix.State.Providers.CosmosDb.Options;
using FluentResults;
using Microsoft.Azure.Cosmos;

namespace Eventix.State.Providers.CosmosDb
{
    [StateProviderName(ProviderName)]
    public class CosmosDbStateProvider : IStateProvider
    {
        private readonly ISystemClock _systemClock;
        private readonly CosmosDbStateConfiguration _configuration;
        private readonly CosmosClient _client;
        public const string ProviderName = "CosmosDb";

        public string Name => ProviderName;

        public CosmosDbStateProvider(ICosmosDbConnectionFactory connectionFactory,
            IOptions<CosmosDbStateConfiguration> configuration,
            ISystemClock systemClock)
        {
            _systemClock = systemClock;
            _configuration = configuration.Value;
            _client = connectionFactory.Create();
        }


        public async Task<Result<IStateEntity>> GetAsync(string storeName, string id, CancellationToken cancellationToken = default)
        {
            var container = GetContainer();
            
            var result = await container.ReadItemAsync<CosmosDbEntity>(id, new PartitionKey(storeName),
                cancellationToken: cancellationToken);
            
            var entity = result.Resource;
            
            return Result.Ok<IStateEntity>(new StateEntity
            {
                Id = entity.Id,
                PartitionKey = entity.PartitionKey,
                Data = entity.Data,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                TimeToLive = entity.TimeToLive
            });
        }

        public async Task<Result<IStateEntity>> CreateAsync(IStateEntity entity,
            CancellationToken cancellationToken = default)
        {
            var now = _systemClock.UtcNow;
            var container = GetContainer();

            var cosmosDbEntity = new CosmosDbEntity
            {
                Id = entity.Id,
                PartitionKey = entity.PartitionKey,
                Data = entity.Data,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                TimeToLive = entity.TimeToLive,
                TTL = entity.TimeToLive.HasValue ? (int)(entity.TimeToLive.Value - now).TotalSeconds : -1
            };

            var result = await container.CreateItemAsync(cosmosDbEntity, new PartitionKey(cosmosDbEntity.PartitionKey),
                cancellationToken: cancellationToken);

            return Result.Ok(entity);
        }

        public async Task<Result<IStateEntity>> UpdateAsync(IStateEntity entity,
            CancellationToken cancellationToken = default)
        {
            var container = GetContainer();

            var cosmosDbEntity = new CosmosDbEntity
            {
                Id = entity.Id,
                PartitionKey = entity.PartitionKey,
                Data = entity.Data,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                TimeToLive = entity.TimeToLive
            };

            var result = await container.UpsertItemAsync(cosmosDbEntity, new PartitionKey(cosmosDbEntity.PartitionKey),
                cancellationToken: cancellationToken);

            return Result.Ok(entity);
        }

        public async Task<Result> DeleteAsync(string storeName, string id, CancellationToken cancellationToken = default)
        {
            var container = GetContainer();

            var result = await container.DeleteItemAsync<CosmosDbEntity>(id, new PartitionKey(storeName),
                cancellationToken: cancellationToken);

            return Result.Ok();
        }

        private Container GetContainer()
        {
            return _client.GetContainer(_configuration.DatabaseName, _configuration.ContainerName);
        }
    }
}