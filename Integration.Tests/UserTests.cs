using Integration.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Tests
{
    [TestClass]
    public class UserTests
    {
        #region private HttpClientHelper _clientHelper;
#pragma warning disable CS8618 // Field configured in the Setup method
        private HttpClientHelper _clientHelper;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        #endregion

        private string _baseAddress = "https://cos730-product-manager.azurewebsites.net/User";
        private string _baseQueueAddress = "https://cos730-product-manager.azurewebsites.net/Queue";
        private string _enqueueOperation = "Enqueue";
        private string _dequeueOperation = "Dequeue";

        [TestInitialize]
        public void Setup()
        {
            _clientHelper = new HttpClientHelper();
        }

        [TestMethod]
        public async Task FullIntegrationTest()
        {
            var queueGuid = Guid.NewGuid();
            var userGuid = Guid.NewGuid();

            // Ensure services are accessible
            await _clientHelper.HealthCheck(_baseAddress);
            await _clientHelper.HealthCheck(_baseQueueAddress);

            // Create a queue item
            var queueItem = GetNewQueueItem(queueGuid);

            // Add a queue
            await _clientHelper.AddItemWithCheck(queueItem, _baseQueueAddress);

            // Ensure queue list has more that one item
            await _clientHelper.EnsureListHasItems<QueueModel>(_baseQueueAddress);

            // Get that queue
            var existingQueue = await _clientHelper.GetAndCheckItem<QueueModel>(_baseQueueAddress, queueItem.id);

            // Create an item
            var item = GetNewItem(userGuid);

            // Add an item
            await _clientHelper.AddItemWithCheck(item, _baseAddress);

            // Ensure item list has more that one item
            await _clientHelper.EnsureListHasItems<User>(_baseAddress);

            // Get that item
            var existingItem = await _clientHelper.GetAndCheckItem<User>(_baseAddress, item.id);

            // Enqueue that item
            await _clientHelper.QueueOperationWithCheck(existingItem.id, existingQueue.id, _baseAddress, _enqueueOperation);

            // Dequeue that item
            await _clientHelper.QueueOperationWithCheck(existingItem.id, existingQueue.id, _baseAddress, _dequeueOperation);

            // Update that item
            await _clientHelper.UpdateItemWithCheck(existingItem, _baseAddress);

            // Delete that item
            await _clientHelper.DeleteItemWithCheck(existingItem.id, _baseAddress);

            // Get that item (should not recieve a response)
            await _clientHelper.GetAndCheckItem<User>(_baseAddress, existingItem.id, false);

            // Delete that queue
            await _clientHelper.DeleteItemWithCheck(existingQueue.id, _baseQueueAddress);

            // Get that queue (should not recieve a response)
            await _clientHelper.GetAndCheckItem<QueueModel>(_baseQueueAddress, existingQueue.id, false);
        }

        #region Private Helper Methods
        private User GetNewItem(Guid guid)
        {
            var item = new User()
            {
                id = guid.ToString(),
                IdNumber = "19960101",
                Email = "test@email.com",
                Username = "TestUser",
                FirstName = "Test",
                LastName = "User",
                Password = "T3st_P@assw0rd",
                Roles = new List<string>().ToArray()
            };

            return item;
        }

        private QueueModel GetNewQueueItem(Guid guid)
        {
            var item = new QueueModel()
            {
                id = guid.ToString(),
                State = "Open",
                TaskIds = new List<string>().ToArray(),
                UserIds = new List<string>().ToArray()
            };

            return item;
        }
        #endregion
    }
}