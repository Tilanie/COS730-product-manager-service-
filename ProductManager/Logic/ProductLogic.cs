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
            return null;
        }

        public async Task<Product> Get(string id)
        {
            return null;
        }

        public async Task<bool> Add(Product product)
        {
            return false;
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
