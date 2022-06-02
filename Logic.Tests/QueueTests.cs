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
    public class QueueTests
    {
        #region private QueueLogic _logic;
#pragma warning disable CS8618 // This field is being set by the test initialisation
        private QueueLogic _logic;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        #endregion

        private CosmosDbMock CosmosDbMock = new CosmosDbMock();

        [TestInitialize]
        public void Setup()
        {
            var dbMock = CosmosDbMock.GetCosmosDbMock();
            var logger = new Mock<ILogger<QueueLogic>>();

            _logic = new QueueLogic(logger.Object, dbMock.Object);
        }

        [TestMethod]
        public async Task TestAdd()
        {
            var item = new QueueModel() { id = "3" };
            var result = await _logic.Add(item);

            Assert.IsTrue(result);
            Assert.AreEqual(3, CosmosDbMock.Queues.Count);
        }

        [TestMethod]
        public async Task TestDelete()
        {
            var result = await _logic.Delete("2");

            Assert.IsTrue(result);
            Assert.IsFalse(CosmosDbMock.Queues.Where(x => x.id == "2").Any());
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
            var state = "CoolState";

            var item = new QueueModel() { id = useId, State = state };
            var result = await _logic.Update(item, useId);

            Assert.IsTrue(result);
            Assert.AreEqual(state, CosmosDbMock.Queues.Where(x => x.id == useId).First().State);
        }

        [TestMethod]
        public async Task TestUpdate_Failure()
        {
            var useId = "8";
            var state = "CoolState";

            var item = new QueueModel() { id = useId, State = state };
            var result = await _logic.Update(item, useId);

            Assert.IsFalse(result);
        }
    }
}