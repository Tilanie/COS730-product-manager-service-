using ProductManager.Models;
using ProductManager.Services;

namespace ProductManager.Logic
{
    public class QueueLogic : IQueueLogic
    {
        private ICosmosDbService _databaseService;
        private readonly ILogger<QueueLogic> _logger;

        public QueueLogic(ILogger<QueueLogic> logger, ICosmosDbService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        public async Task<List<QueueModel>> List()
        {
            return (await _databaseService.GetItemsAsync<QueueModel>(DataLayerType.Queue)).ToList();
        }

        public async Task<QueueModel> Get(string id)
        {
            var queue = await _databaseService.GetItemAsync<QueueModel>(id, DataLayerType.Queue);

            if (queue != null)
            {
                var defaultList = new List<string>();
                var tasks = new List<TaskModel>();
                foreach (var taskId in queue.TaskIds ?? defaultList.ToArray())
                {
                    tasks.Add(await _databaseService.GetItemAsync<TaskModel>(taskId, DataLayerType.Task));
                }
                queue.Tasks = tasks.ToArray();

                var users = new List<User>();
                foreach (var userId in queue.UserIds ?? defaultList.ToArray())
                {
                    users.Add(await _databaseService.GetItemAsync<User>(userId, DataLayerType.User));
                }
                queue.Users = users.ToArray();
            }

            return queue;
        }

        public async Task<bool> Add(QueueModel queue)
        {
            return false;
        }

        public async Task<bool> Update(QueueModel queue, string id)
        {
            return false;
        }

        public async Task<bool> Delete(string id)
        {
            return false;
        }
    }
}
