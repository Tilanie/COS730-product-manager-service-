using ProductManager.Models;
using ProductManager.Services;

namespace ProductManager.Logic
{
    public class ProductLogic : IProductLogic
    {
        private ICosmosDbService _databaseService;
        private readonly ILogger<ProductLogic> _logger;

        public ProductLogic(ILogger<ProductLogic> logger, ICosmosDbService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        public async Task<List<Product>> List()
        {
            return (await _databaseService.GetItemsAsync<Product>(DataLayerType.Product)).ToList();
        }

        public async Task<Product> Get(string id)
        {
            return await _databaseService.GetItemAsync<Product>(id, DataLayerType.Product);
        }

        public async Task<bool> Add(Product product)
        {
            try
            {
                await _databaseService.AddItemAsync(product, DataLayerType.Product);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new Product");
                return false;
            }
        }

        public async Task<bool> Update(Product product, string id)
        {
            return false;
        }

        public async Task<bool> Delete(string id)
        {
            return false;
        }
    }
}
