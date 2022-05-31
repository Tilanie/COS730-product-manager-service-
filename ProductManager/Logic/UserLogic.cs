using ProductManager.Models;
using ProductManager.Services;

namespace ProductManager.Logic
{
    public class UserLogic : IUserLogic
    {
        private ICosmosDbService _databaseService;
        private readonly ILogger<UserLogic> _logger;
        private readonly IQueueLogic _queueLogic;

        public UserLogic(ILogger<UserLogic> logger, ICosmosDbService databaseService, IQueueLogic queueLogic)
        {
            _logger = logger;
            _databaseService = databaseService;
            _queueLogic = queueLogic;
        }

        public async Task<List<User>> List()
        {
            return null;
        }

        public async Task<User> Get(string id)
        {
            return null;
        }

        public async Task<bool> Add(User user)
        {
            return false;
        }

        public async Task<bool> Update(User user, string id)
        {
            return false;

        }

        public async Task<bool> Delete(string id)
        {
            return false;
        }

        public async Task<bool> Enqueue(string queueId, string userId)
        {
            return false;
        }

        public async Task<bool> Dequeue(string queueId, string userId)
        {
            return false;
        }
    }
}
