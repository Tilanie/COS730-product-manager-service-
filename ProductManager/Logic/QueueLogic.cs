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
            return null;
        }

        public async Task<QueueModel> Get(string id)
        {
            return null;
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
