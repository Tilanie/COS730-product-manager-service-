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
            try
            {
                var queue = await _queueLogic.Get(queueId);

                var taskIdList = queue.TaskIds.ToList();
                taskIdList.Add(taskId);

                queue.TaskIds = taskIdList.Distinct().ToArray();

                return await _queueLogic.Update(queue, queueId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error enqueueing task with id: {taskId}");
                return false;
            }
        }

        public async Task<bool> Dequeue(string queueId, string taskId)
        {
            try
            {
                var queue = await _queueLogic.Get(queueId);

                var taskIdList = queue.TaskIds.ToList();
                taskIdList.Remove(taskId);

                queue.TaskIds = taskIdList.Distinct().ToArray();

                return await _queueLogic.Update(queue, queueId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error dequeueing task with id: {taskId}");
                return false;
            }
        }

        public async Task<bool> Complete(string queueId, string taskId)
        {
            try
            {
                var dequeueResponse = await Dequeue(queueId, taskId);
                var task = await Get(taskId);
                task.Status = "Completed";

                var updateResponse = await Update(task, taskId);

                return (dequeueResponse && updateResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error completing task with id: {taskId}");
                return false;
            }
        }

        public async Task<List<TaskModel>> List()
        {
            return (await _databaseService.GetItemsAsync<TaskModel>(DataLayerType.Task)).ToList();
        }

        public async Task<TaskModel> Get(string id)
        {
            return await _databaseService.GetItemAsync<TaskModel>(id, DataLayerType.Task);
        }

        public async Task<bool> Add(TaskModel task)
        {
            try
            {
                await _databaseService.AddItemAsync(task, DataLayerType.Task);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new Task");
                return false;
            }
        }

        private async Task<bool> Update(TaskModel task, string id)
        {
            try
            {
                await _databaseService.UpdateItemAsync(id, task);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new Task");
                return false;
            }
        }
    }
}
