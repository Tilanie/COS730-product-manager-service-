using Microsoft.AspNetCore.Mvc;
using ProductManager.Logic;
using ProductManager.Models;
using ProductManager.Services;

namespace ProductManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductLogic _productLogic;

        public ProductController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        [Route("Health")]
        [HttpGet]
        public async Task<string> Health()
        {
            return "The Product Service is Healthy :)";
        }

        [Route("List")]
        [HttpGet]
        public async Task<IEnumerable<Product>> List()
        {
            return await _productLogic.List();
        }

        [Route("Get/{id}")]
        [HttpGet]
        public async Task<Product> Get(string id)
        {
            return await _productLogic.Get(id);
        }

        [Route("Add")]
        [HttpPost]
        public async Task<bool> Add([FromBody] Product product)
        {
            return await _productLogic.Add(product);
        }

        [Route("Update/{id}")]
        [HttpPost]
        public async Task<bool> Update([FromBody] Product product, string id)
        {
            return await _productLogic.Update(product, id);
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<bool> Delete(string id)
        {
            return await _productLogic.Delete(id);
        }
    }
}