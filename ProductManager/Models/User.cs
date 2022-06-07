using Newtonsoft.Json;

namespace ProductManager.Models
{
    public class User: BaseCosmosItem
    {
        [JsonProperty(PropertyName = "IdNumber")]
        public string IdNumber { get; set; }

        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "Roles")]
        public string[] Roles { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

        public override DataLayerType DataLayerType => DataLayerType.User;
    }
}
