using ProductManager.Models;

namespace ProductManager.Logic
{
    public interface IUserLogic
    {
        Task<bool> Add(User user);
        Task<bool> Delete(string id);
        Task<User> Get(string id);
        Task<List<User>> List();
        Task<bool> Update(User user, string id);
        Task<bool> Enqueue(string queueId, string userId);
        Task<bool> Dequeue(string queueId, string userId);
    }
}