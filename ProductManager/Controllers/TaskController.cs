using Microsoft.AspNetCore.Mvc;
using ProductManager.Logic;
using ProductManager.Models;
using ProductManager.Services;

namespace ProductManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskLogic _taskLogic;

        public TaskController(ITaskLogic taskLogic)
        {
            _taskLogic = taskLogic;
        }

        [Route("Health")]
        [HttpGet]
        public async Task<string> Health()
        {
            return "The Task Service is Healthy :)";
        }

        [Route("Enqueue/{queueId}/{taskId}")]
        [HttpGet]
        public async Task<bool> Enqueue(string queueId, string taskId)
        {
            return await _taskLogic.Enqueue(queueId, taskId);
        }

        [Route("Dequeue/{queueId}/{taskId}")]
        [HttpGet]
        public async Task<bool> Dequeue(string queueId, string taskId)
        {
            return await _taskLogic.Dequeue(queueId, taskId);
        }

        [Route("Complete/{queueId}/{taskId}")]
        [HttpGet]
        public async Task<bool> Complete(string queueId, string taskId)
        {
            return await _taskLogic.Complete(queueId, taskId);
        }

        [Route("List")]
        [HttpGet]
        public async Task<IEnumerable<TaskModel>> List()
        {
            return await _taskLogic.List();
        }

        [Route("Get/{id}")]
        [HttpGet]
        public async Task<TaskModel> Get(string id)
        {
            return await _taskLogic.Get(id);
        }

        [Route("Add")]
        [HttpPost]
        public async Task<bool> Add([FromBody] TaskModel task)
        {
            return await _taskLogic.Add(task);
        }
    }
}
