#nullable disable

using ProductManager.Models;
using Microsoft.Azure.Cosmos;

namespace ProductManager.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync<T>(T item, DataLayerType type)
        {
            await this._container.CreateItemAsync<T>(item, new PartitionKey((int)type));
        }

        public async Task DeleteItemAsync<T>(string id, DataLayerType type)
        {
            await this._container.DeleteItemAsync<T>(id, new PartitionKey((int)type));
        }

        public async Task<T> GetItemAsync<T>(string id, DataLayerType type)
        {
            try
            {
                ItemResponse<T> response = await this._container.ReadItemAsync<T>(id, new PartitionKey((int)type));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(T);
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(DataLayerType type)
        {
            var query = this._container.GetItemQueryIterator<T>(new QueryDefinition($"SELECT * FROM c WHERE c.DataLayerType = {(int)type}"));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync<T>(string id, T item)
        {
            await this._container.ReplaceItemAsync<T>(item, id);
        }
    }
}