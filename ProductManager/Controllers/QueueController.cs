using Microsoft.AspNetCore.Mvc;
using ProductManager.Logic;
using ProductManager.Models;

namespace ProductManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {
        private IQueueLogic _queueLogic;

        public QueueController(IQueueLogic queueLogic)
        {
            _queueLogic = queueLogic;
        }

        [Route("Health")]
        [HttpGet]
        public async Task<string> Health()
        {
            return "The Queue Service is Healthy :)";
        }

        [Route("List")]
        [HttpGet]
        public async Task<IEnumerable<QueueModel>> List()
        {
            return await _queueLogic.List();
        }

        [Route("Get/{id}")]
        [HttpGet]
        public async Task<QueueModel> Get(string id)
        {
            return await _queueLogic.Get(id);
        }

        [Route("Add")]
        [HttpPost]
        public async Task<bool> Add(QueueModel queue)
        {
            return await _queueLogic.Add(queue);
        }

        [Route("Update/{id}")]
        [HttpPost]
        public async Task<bool> Update(QueueModel queue, string id)
        {
            return await _queueLogic.Update(queue, id);
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<bool> Delete(string id)
        {
            return await _queueLogic.Delete(id);
        }
    }
}
