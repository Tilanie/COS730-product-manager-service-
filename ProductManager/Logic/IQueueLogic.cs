using ProductManager.Models;

namespace ProductManager.Logic
{
    public interface IQueueLogic
    {
        Task<bool> Add(QueueModel queue);
        Task<bool> Delete(string id);
        Task<QueueModel> Get(string id);
        Task<List<QueueModel>> List();
        Task<bool> Update(QueueModel queue, string id);
    }
}