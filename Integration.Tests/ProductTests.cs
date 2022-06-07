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
    public class ProductTests
    {
        #region private HttpClientHelper _clientHelper;
#pragma warning disable CS8618 // Field configured in the Setup method
        private HttpClientHelper _clientHelper;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        #endregion

        private string _baseAddress = "https://cos730-product-manager.azurewebsites.net/Product";

        [TestInitialize]
        public void Setup()
        {
            _clientHelper = new HttpClientHelper();
        }

        [TestMethod]
        public async Task FullIntegrationTest()
        {
            var userGuid = Guid.NewGuid();

            // Ensure services are accessible
            await _clientHelper.HealthCheck(_baseAddress);

            // Create an item
            var item = GetNewItem(userGuid);

            // Add an item
            await _clientHelper.AddItemWithCheck(item, _baseAddress);

            // Ensure item list has more that one item
            await _clientHelper.EnsureListHasItems<Product>(_baseAddress);

            // Get that item
            var existingItem = await _clientHelper.GetAndCheckItem<Product>(_baseAddress, item.id);

            // Update that item
            await _clientHelper.UpdateItemWithCheck(existingItem, _baseAddress);

            // Delete that item
            await _clientHelper.DeleteItemWithCheck(existingItem.id, _baseAddress);

            // Get that item (should not recieve a response)
            await _clientHelper.GetAndCheckItem<Product>(_baseAddress, existingItem.id, false);
        }

        #region Private Helper Methods
        private Product GetNewItem(Guid guid)
        {
            var item = new Product()
            {
                id = guid.ToString(),
                Created = DateTime.Now,
                ProductID = Guid.NewGuid(),
                VendorID = Guid.NewGuid(),
                Size = 12,
                UnitPrice = 12,
                ProductName = "Test",
                Colour = "Red",
                Category = "Test",
                Brand = "Test",
                Description = "Test",
                StockLevel = 12
            };

            return item;
        }
        #endregion
    }
}