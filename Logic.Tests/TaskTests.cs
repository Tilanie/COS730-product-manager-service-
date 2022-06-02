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
    public class TaskTests
    {
        #region private UserLogic _logic;
#pragma warning disable CS8618 // This field is being set by the test initialisation
        private TaskLogic _logic;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        #endregion

        private CosmosDbMock CosmosDbMock = new CosmosDbMock();

        [TestInitialize]
        public void Setup()
        {
            var dbMock = CosmosDbMock.GetCosmosDbMock();
            var logger = new Mock<ILogger<TaskLogic>>();
            var queueLogger = new Mock<ILogger<QueueLogic>>();

            var queueLogic = new QueueLogic(queueLogger.Object, dbMock.Object);
            _logic = new TaskLogic(logger.Object, dbMock.Object, queueLogic);
        }

        [TestMethod]
        public async Task TestAdd()
        {
            var item = new TaskModel() { id = "3" };
            var result = await _logic.Add(item);

            Assert.IsTrue(result);
            Assert.AreEqual(3, CosmosDbMock.Tasks.Count);
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
        public async Task TestEnqueue()
        {
            var task = new TaskModel() { id = "3" };
            var addResult = await _logic.Add(task);

            Assert.IsTrue(addResult);

            var result = await _logic.Enqueue("1", "3");

            Assert.IsTrue(result);
            Assert.IsTrue(CosmosDbMock.Queues.Where(x => x.id == "1").First().TaskIds.Contains("3"));
        }

        [TestMethod]
        public async Task TestDequeue()
        {
            var result = await _logic.Dequeue("1", "2");

            Assert.IsTrue(result);
            Assert.IsFalse(CosmosDbMock.Queues.Where(x => x.id == "1").First().TaskIds.Contains("2"));
        }

        [TestMethod]
        public async Task TestComplete()
        {
            var result = await _logic.Complete("1", "2");

            Assert.IsTrue(result);
            Assert.IsFalse(CosmosDbMock.Queues.Where(x => x.id == "1").First().TaskIds.Contains("2"));
            Assert.AreEqual("Completed", CosmosDbMock.Tasks.Where(x => x.id == "2").First().Status);
        }
    }
}