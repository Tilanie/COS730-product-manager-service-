using ProductManager.Models;

namespace ProductManager.Logic
{
    public interface IProductLogic
    {
        Task<bool> Add(Product product);
        Task<bool> Delete(string id);
        Task<Product> Get(string id);
        Task<List<Product>> List();
        Task<bool> Update(Product product, string id);
    }
}