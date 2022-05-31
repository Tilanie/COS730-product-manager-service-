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
            return (await _databaseService.GetItemsAsync<User>(DataLayerType.User)).ToList();
        }

        public async Task<User> Get(string id)
        {
            return await _databaseService.GetItemAsync<User>(id, DataLayerType.User);
        }

        public async Task<bool> Add(User user)
        {
            try
            {
                await _databaseService.AddItemAsync(user, DataLayerType.User);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new Product");
                return false;
            }
        }

        public async Task<bool> Update(User user, string id)
        {
            try
            {
                await _databaseService.UpdateItemAsync(id, user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with id: {id}");
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                await _databaseService.DeleteItemAsync<User>(id, DataLayerType.User);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with id: {id}");
                return false;
            }
        }

       
    }
}
