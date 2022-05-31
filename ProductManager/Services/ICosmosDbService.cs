using ProductManager.Models;

namespace ProductManager.Services
{
    public interface ICosmosDbService
    {
        Task AddItemAsync<T>(T item, DataLayerType type);
        Task DeleteItemAsync<T>(string id, DataLayerType type);
        Task<T> GetItemAsync<T>(string id, DataLayerType type);
        Task<IEnumerable<T>> GetItemsAsync<T>(DataLayerType type);
        Task UpdateItemAsync<T>(string id, T item);
    }
}
