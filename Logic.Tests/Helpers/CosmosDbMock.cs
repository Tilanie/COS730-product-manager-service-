using Moq;
using ProductManager.Models;
using ProductManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Tests.Helpers
{
    public class CosmosDbMock
    {
        public List<Product> Products = new List<Product>();
        public List<QueueModel> Queues = new List<QueueModel>();
        public List<TaskModel> Tasks = new List<TaskModel>();
        public List<User> Users = new List<User>();

        public Mock<ICosmosDbService> GetCosmosDbMock()
        {
            var mock = new Mock<ICosmosDbService>();

            // Setup the add methods
            SetupAdds(mock);

            // Setup the delete methods
            SetupDeletes(mock);

            // Setup the delete methods
            SetupGets(mock);

            // Setup the delete methods
            SetupGetLists(mock);

            // Setup the update methods
            SetupUpdates(mock);

            // Setup the Databases
            SetupProducts();
            SetupQueues();
            SetupTasks();
            SetupUsers();

            return mock;
        }

        #region Mock Setups
        private void SetupAdds(Mock<ICosmosDbService> mock)
        {
            mock.Setup(x => x.AddItemAsync(It.IsAny<Product>(), It.IsAny<DataLayerType>()))
                .Returns((Product item, DataLayerType type) =>
                {
                    return AddProductAsync(item, type);
                });

            mock.Setup(x => x.AddItemAsync(It.IsAny<TaskModel>(), It.IsAny<DataLayerType>()))
                .Returns((TaskModel item, DataLayerType type) =>
                {
                    return AddTaskAsync(item, type);
                });

            mock.Setup(x => x.AddItemAsync(It.IsAny<QueueModel>(), It.IsAny<DataLayerType>()))
                .Returns((QueueModel item, DataLayerType type) =>
                {
                    return AddQueueAsync(item, type);
                });

            mock.Setup(x => x.AddItemAsync(It.IsAny<User>(), It.IsAny<DataLayerType>()))
                .Returns((User item, DataLayerType type) =>
                {
                    return AddUserAsync(item, type);
                });
        }

        private void SetupDeletes(Mock<ICosmosDbService> mock)
        {
            mock.Setup(x => x.DeleteItemAsync<Product>(It.IsAny<string>(), It.IsAny<DataLayerType>()))
                .Returns((string item, DataLayerType type) =>
                {
                    return DeleteProductAsync(item, type);
                });

            mock.Setup(x => x.DeleteItemAsync<TaskModel>(It.IsAny<string>(), It.IsAny<DataLayerType>()))
                .Returns((string item, DataLayerType type) =>
                {
                    return DeleteTaskAsync(item, type);
                });

            mock.Setup(x => x.DeleteItemAsync<QueueModel>(It.IsAny<string>(), It.IsAny<DataLayerType>()))
                .Returns((string item, DataLayerType type) =>
                {
                    return DeleteQueueAsync(item, type);
                });

            mock.Setup(x => x.DeleteItemAsync<User>(It.IsAny<string>(), It.IsAny<DataLayerType>()))
                .Returns((string item, DataLayerType type) =>
                {
                    return DeleteUserAsync(item, type);
                });
        }

        private void SetupGets(Mock<ICosmosDbService> mock)
        {
            mock.Setup(x => x.GetItemAsync<Product>(It.IsAny<string>(), It.IsAny<DataLayerType>()))
                .Returns((string item, DataLayerType type) =>
                {
                    return GetProductAsync(item, type);
                });

            mock.Setup(x => x.GetItemAsync<TaskModel>(It.IsAny<string>(), It.IsAny<DataLayerType>()))
                .Returns((string item, DataLayerType type) =>
                {
                    return GetTaskAsync(item, type);
                });

            mock.Setup(x => x.GetItemAsync<QueueModel>(It.IsAny<string>(), It.IsAny<DataLayerType>()))
                .Returns((string item, DataLayerType type) =>
                {
                    return GetQueueAsync(item, type);
                });

            mock.Setup(x => x.GetItemAsync<User>(It.IsAny<string>(), It.IsAny<DataLayerType>()))
                .Returns((string item, DataLayerType type) =>
                {
                    return GetUserAsync(item, type);
                });
        }

        private void SetupGetLists(Mock<ICosmosDbService> mock)
        {
            mock.Setup(x => x.GetItemsAsync<Product>(It.IsAny<DataLayerType>()))
                .Returns((DataLayerType type) =>
                {
                    return Task.FromResult((IEnumerable<Product>)Products);
                });

            mock.Setup(x => x.GetItemsAsync<TaskModel>(It.IsAny<DataLayerType>()))
                .Returns((DataLayerType type) =>
                {
                    return Task.FromResult((IEnumerable<TaskModel>)Tasks);
                });

            mock.Setup(x => x.GetItemsAsync<QueueModel>(It.IsAny<DataLayerType>()))
                .Returns((DataLayerType type) =>
                {
                    return Task.FromResult((IEnumerable<QueueModel>)Queues);
                });

            mock.Setup(x => x.GetItemsAsync<User>(It.IsAny<DataLayerType>()))
                .Returns((DataLayerType type) =>
                {
                    return Task.FromResult((IEnumerable<User>)Users);
                });
        }

        private void SetupUpdates(Mock<ICosmosDbService> mock)
        {
            mock.Setup(x => x.UpdateItemAsync(It.IsAny<string>(), It.IsAny<Product>()))
                .Returns((string id, Product item) =>
                {
                    return UpdateProductAsync(id, item);
                });
            
            mock.Setup(x => x.UpdateItemAsync(It.IsAny<string>(), It.IsAny<TaskModel>()))
                 .Returns((string id, TaskModel item) =>
                 {
                     return UpdateTaskAsync(id, item);
                 });

            mock.Setup(x => x.UpdateItemAsync(It.IsAny<string>(), It.IsAny<QueueModel>()))
                 .Returns((string id, QueueModel item) =>
                 {
                     return UpdateQueueAsync(id, item);
                 });

            mock.Setup(x => x.UpdateItemAsync(It.IsAny<string>(), It.IsAny<User>()))
                 .Returns((string id, User item) =>
                 {
                     return UpdateUserAsync(id, item);
                 });
        }
        #endregion

        #region Mock DB Functions

        #region Mock Adds
        private async Task AddProductAsync(Product item, DataLayerType type)
        {
            await Task.Delay(1);
            Products.Add(item);
        }
        private async Task AddTaskAsync(TaskModel item, DataLayerType type)
        {
            await Task.Delay(1);
            Tasks.Add(item);
        }
        private async Task AddQueueAsync(QueueModel item, DataLayerType type)
        {
            await Task.Delay(1);
            Queues.Add(item);
        }
        private async Task AddUserAsync(User item, DataLayerType type)
        {
            await Task.Delay(1);
            Users.Add(item);
        }
        #endregion

        #region Mock Deletes
        private async Task DeleteProductAsync(string id, DataLayerType type)
        {
            await Task.Delay(1);
            var item = Products.Where(x => x.id == id).FirstOrDefault();

            if (item == null)
            {
                throw new ArgumentNullException($"Product with id: {id} does not exist!");
            }

            Products.Remove(item);
        }
        private async Task DeleteTaskAsync(string id, DataLayerType type)
        {
            await Task.Delay(1);
            var item = Tasks.Where(x => x.id == id).FirstOrDefault();

            if (item == null)
            {
                throw new ArgumentNullException($"Task with id: {id} does not exist!");
            }

            Tasks.Remove(item);
        }
        private async Task DeleteQueueAsync(string id, DataLayerType type)
        {
            await Task.Delay(1);
            var item = Queues.Where(x => x.id == id).FirstOrDefault();

            if (item == null)
            {
                throw new ArgumentNullException($"Queue with id: {id} does not exist!");
            }

            Queues.Remove(item);
        }
        private async Task DeleteUserAsync(string id, DataLayerType type)
        {
            await Task.Delay(1);
            var item = Users.Where(x => x.id == id).FirstOrDefault();

            if (item == null)
            {
                throw new ArgumentNullException($"User with id: {id} does not exist!");
            }

            Users.Remove(item);
        }
        #endregion

        #region Mock Gets
        private async Task<Product> GetProductAsync(string id, DataLayerType type)
        {
            await Task.Delay(1);
            var item = Products.Where(x => x.id == id).FirstOrDefault();
            return item;
        }
        private async Task<TaskModel> GetTaskAsync(string id, DataLayerType type)
        {
            await Task.Delay(1);
            var item = Tasks.Where(x => x.id == id).FirstOrDefault();
            return item;
        }
        private async Task<QueueModel> GetQueueAsync(string id, DataLayerType type)
        {
            await Task.Delay(1);
            var item = Queues.Where(x => x.id == id).FirstOrDefault();
            return item;
        }
        private async Task<User> GetUserAsync(string id, DataLayerType type)
        {
            await Task.Delay(1);
            var item = Users.Where(x => x.id == id).FirstOrDefault();
            return item;
        }
        #endregion

        #region Mock Updates
        private async Task UpdateProductAsync(string id, Product item)
        {
            await Task.Delay(1);
            var existingItem = Products.Where(x => x.id == id).FirstOrDefault();

            if (existingItem == null)
            {
                throw new ArgumentNullException($"Product with id: {id} does not exist!");
            }

            Products.Remove(existingItem);
            Products.Add(item);
        }
        private async Task UpdateTaskAsync(string id, TaskModel item)
        {
            await Task.Delay(1);
            var existingItem = Tasks.Where(x => x.id == id).FirstOrDefault();

            if (existingItem == null)
            {
                throw new ArgumentNullException($"Task with id: {id} does not exist!");
            }

            Tasks.Remove(existingItem);
            Tasks.Add(item);
        }
        private async Task UpdateQueueAsync(string id, QueueModel item)
        {
            await Task.Delay(1);
            var existingItem = Queues.Where(x => x.id == id).FirstOrDefault();

            if (existingItem == null)
            {
                throw new ArgumentNullException($"Queue with id: {id} does not exist!");
            }

            Queues.Remove(existingItem);
            Queues.Add(item);
        }
        private async Task UpdateUserAsync(string id, User item)
        {
            await Task.Delay(1);
            var existingItem = Users.Where(x => x.id == id).FirstOrDefault();

            if (existingItem == null)
            {
                throw new ArgumentNullException($"User with id: {id} does not exist!");
            }

            Users.Remove(existingItem);
            Users.Add(item);
        }
        #endregion

        #endregion

        #region Mock Database Setup
        private void SetupProducts()
        {
            Products.Add(new Product() { id = "1" });
            Products.Add(new Product() { id = "2" });
        }

        private void SetupQueues()
        {
            var idList = new List<string>() { "1", "2" };

            Queues.Add(new QueueModel() { id = "1", TaskIds = idList.ToArray(), UserIds = idList.ToArray() });
            Queues.Add(new QueueModel() { id = "2", TaskIds = idList.ToArray(), UserIds = idList.ToArray() });
        }

        private void SetupTasks()
        {
            Tasks.Add(new TaskModel() { id = "1" });
            Tasks.Add(new TaskModel() { id = "2" });
        }

        private void SetupUsers()
        {
            Users.Add(new User() { id = "1" });
            Users.Add(new User() { id = "2" });
        }
        #endregion
    }
}
