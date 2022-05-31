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
          
        }

        public async Task DeleteItemAsync<T>(string id, DataLayerType type)
        {
            
        }

        public async Task<T> GetItemAsync<T>(string id, DataLayerType type)
        {
            return default(T);
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(DataLayerType type)
        {
            return null;
        }

        public async Task UpdateItemAsync<T>(string id, T item)
        {
           
        }
    }
}