
using ProductManager.Models;

namespace ProductManager.Logic
{
    public interface ITaskLogic
    {
        Task<bool> Add(TaskModel task);
        Task<bool> Complete(string queueId, string taskId);
        Task<bool> Dequeue(string queueId, string taskId);
        Task<bool> Enqueue(string queueId, string taskId);
        Task<TaskModel> Get(string id);
        Task<List<TaskModel>> List();
    }
}