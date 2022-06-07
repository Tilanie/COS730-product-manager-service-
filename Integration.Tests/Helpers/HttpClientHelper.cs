using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Tests.Helpers
{
    public class HttpClientHelper
    {
        private HttpClient _httpClient;

        public HttpClientHelper()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept
                           .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task HealthCheck(string baseAddress)
        {
            var response = await _httpClient.GetAsync($"{baseAddress}/Health");

            if (response.IsSuccessStatusCode)
            {
                var success = await response.Content.ReadAsStringAsync();
                Assert.IsTrue(success != String.Empty);
            }
            else
            {
                var fullError = await response.Content.ReadAsStringAsync();
                Assert.Fail($"{response.StatusCode}: {response.ReasonPhrase} - {fullError}");
            }
        }

        public async Task AddItemWithCheck<T>(T item, string baseAddress) where T : BaseCosmosItem
        {
            var response = await _httpClient.PostAsync($"{baseAddress}/Add", GetContent(JsonConvert.SerializeObject(item)));

            if (response.IsSuccessStatusCode)
            {
                var success = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                Assert.IsTrue(success);
            }
            else
            {
                var fullError = await response.Content.ReadAsStringAsync();
                Assert.Fail($"{response.StatusCode}: {response.ReasonPhrase} - {fullError}");
            }
        }

        public async Task UpdateItemWithCheck<T>(T item, string baseAddress) where T : BaseCosmosItem
        {
            var response = await _httpClient.PostAsync($"{baseAddress}/Update/{item.id}", GetContent(JsonConvert.SerializeObject(item)));

            if (response.IsSuccessStatusCode)
            {
                var success = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                Assert.IsTrue(success);
            }
            else
            {
                var fullError = await response.Content.ReadAsStringAsync();
                Assert.Fail($"{response.StatusCode}: {response.ReasonPhrase} - {fullError}");
            }
        }

        public async Task DeleteItemWithCheck(string id, string baseAddress)
        {
            var response = await _httpClient.DeleteAsync($"{baseAddress}/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                var success = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                Assert.IsTrue(success);
            }
            else
            {
                var fullError = await response.Content.ReadAsStringAsync();
                Assert.Fail($"{response.StatusCode}: {response.ReasonPhrase} - {fullError}");
            }
        }

        public async Task QueueOperationWithCheck(string id, string queueId, string baseAddress, string operation)
        {
            var response = await _httpClient.GetAsync($"{baseAddress}/{operation}/{queueId}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var success = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                Assert.IsTrue(success);
            }
            else
            {
                var fullError = await response.Content.ReadAsStringAsync();
                Assert.Fail($"{response.StatusCode}: {response.ReasonPhrase} - {fullError}");
            }
        }

        public async Task EnsureListHasItems<T>(string baseAddress)
        {
            var response = await _httpClient.GetAsync($"{baseAddress}/List");

            if (response.IsSuccessStatusCode)
            {
                var items = JsonConvert.DeserializeObject<List<T>>(await response.Content.ReadAsStringAsync());
                Assert.IsNotNull(items);
                Assert.IsTrue(items.Count > 0);
            }
            else
            {
                var fullError = await response.Content.ReadAsStringAsync();
                Assert.Fail($"{response.StatusCode}: {response.ReasonPhrase} - {fullError}");
            }
        }

        public async Task<T> GetAndCheckItem<T>(string baseAddress, string id, bool checkExists = true) where T : BaseCosmosItem
        {
            var response = await _httpClient.GetAsync($"{baseAddress}/Get/{id}");

            if (response.IsSuccessStatusCode)
            {
                var existingItem = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

                if (checkExists)
                {
                    Assert.IsNotNull(existingItem);
                    Assert.IsTrue(existingItem.id == id);
                }
                else
                {
                    Assert.IsNull(existingItem);
                }

                return existingItem;
            }
            else
            {
                var fullError = await response.Content.ReadAsStringAsync();
                Assert.Fail($"{response.StatusCode}: {response.ReasonPhrase} - {fullError}");
            }

            #region return default(T);
#pragma warning disable CS8603 // Possible null reference return.
            return default(T);
#pragma warning restore CS8603 // Possible null reference return.
            #endregion
        }

        public ByteArrayContent GetContent(string myContent)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }
    }
}
