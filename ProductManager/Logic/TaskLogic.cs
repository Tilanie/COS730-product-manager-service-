using ProductManager.Models;
using ProductManager.Services;

namespace ProductManager.Logic
{
    public class TaskLogic : ITaskLogic
    {
        private ICosmosDbService _databaseService;
        private readonly ILogger<TaskLogic> _logger;
        private readonly IQueueLogic _queueLogic;

        public TaskLogic(ILogger<TaskLogic> logger, ICosmosDbService databaseService, IQueueLogic queueLogic)
        {
            _logger = logger;
            _databaseService = databaseService;
            _queueLogic = queueLogic;
        }

        public async Task<bool> Enqueue(string queueId, string taskId)
        {
            return false;
        }

        public async Task<bool> Dequeue(string queueId, string taskId)
        {
            return false;
        }

        public async Task<bool> Complete(string queueId, string taskId)
        {
            return false;
        }

        public async Task<List<TaskModel>> List()
        {
            return null;
        }

        public async Task<TaskModel> Get(string id)
        {
            return null;
        }

        public async Task<bool> Add(TaskModel task)
        {
            return false;
        }

        private async Task<bool> Update(TaskModel task, string id)
        {
            return false;
        }
    }
}
