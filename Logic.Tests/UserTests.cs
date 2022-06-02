using Logic.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductManager.Logic;
using ProductManager.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Tests
{
    [TestClass]
    public class UserTests
    {
        #region private UserLogic _logic;
#pragma warning disable CS8618 // This field is being set by the test initialisation
        private UserLogic _logic;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        #endregion

        private CosmosDbMock CosmosDbMock = new CosmosDbMock();

        [TestInitialize]
        public void Setup()
        {
            var dbMock = CosmosDbMock.GetCosmosDbMock();
            var logger = new Mock<ILogger<UserLogic>>();
            var queueLogger = new Mock<ILogger<QueueLogic>>();

            var queueLogic = new QueueLogic(queueLogger.Object, dbMock.Object);
            _logic = new UserLogic(logger.Object, dbMock.Object, queueLogic);
        }

        [TestMethod]
        public async Task TestAdd()
        {
            var item = new User() { id = "3" };
            var result = await _logic.Add(item);

            Assert.IsTrue(result);
            Assert.AreEqual(3, CosmosDbMock.Users.Count);
        }

        [TestMethod]
        public async Task TestDelete()
        {
            var result = await _logic.Delete("2");

            Assert.IsTrue(result);
            Assert.IsFalse(CosmosDbMock.Users.Where(x => x.id == "2").Any());
        }

        [TestMethod]
        public async Task TestDelete_Failure()
        {
            var result = await _logic.Delete("3");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task TestGet()
        {
            var result = await _logic.Get("2");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestGet_WithNoResult()
        {
            var result = await _logic.Get("4");

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestList()
        {
            var result = await _logic.List();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            var useId = "1";
            var name = "CoolName";

            var item = new User() { id = useId, FirstName = name };
            var result = await _logic.Update(item, useId);

            Assert.IsTrue(result);
            Assert.AreEqual(name, CosmosDbMock.Users.Where(x => x.id == useId).First().FirstName);
        }

        [TestMethod]
        public async Task TestUpdate_Failure()
        {
            var useId = "8";
            var name = "CoolName";

            var item = new User() { id = useId, FirstName = name };
            var result = await _logic.Update(item, useId);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task TestEnqueue()
        {
            var user = new User() { id = "3" };
            var addResult = await _logic.Add(user);

            Assert.IsTrue(addResult);

            var result = await _logic.Enqueue("1", "3");

            Assert.IsTrue(result);
            Assert.IsTrue(CosmosDbMock.Queues.Where(x => x.id == "1").First().UserIds.Contains("3"));
        }

        [TestMethod]
        public async Task TestDequeue()
        {
            var result = await _logic.Dequeue("1", "2");

            Assert.IsTrue(result);
            Assert.IsFalse(CosmosDbMock.Queues.Where(x => x.id == "1").First().UserIds.Contains("2"));
        }
    }
}