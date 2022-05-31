using Microsoft.AspNetCore.Mvc;
using ProductManager.Logic;
using ProductManager.Models;

namespace ProductManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserLogic _userLogic;
        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [Route("Health")]
        [HttpGet]
        public async Task<string> Health()
        {
            return "The User Service is Healthy :)";
        }

        [Route("List")]
        [HttpGet]
        public async Task<IEnumerable<User>> List()
        {
            return await _userLogic.List();
        }

        [Route("Get/{id}")]
        [HttpGet]
        public async Task<User> Get(string id)
        {
            return await _userLogic.Get(id);
        }

        [Route("Add")]
        [HttpPost]
        public async Task<bool> Add(User user)
        {
            return await _userLogic.Add(user);
        }

        [Route("Update/{id}")]
        [HttpPost]
        public async Task<bool> Update(User user, string id)
        {
            return await _userLogic.Update(user, id);
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<bool> Delete(string id)
        {
            return await _userLogic.Delete(id);
        }

        [Route("Enqueue/{queueId}/{userId}")]
        [HttpGet]
        public async Task<bool> Enqueue(string queueId, string userId)
        {
            return await _userLogic.Enqueue(queueId, userId);
        }

        [Route("Dequeue/{queueId}/{userId}")]
        [HttpGet]
        public async Task<bool> Dequeue(string queueId, string userId)
        {
            return await _userLogic.Dequeue(queueId, userId);
        }
    }
}
