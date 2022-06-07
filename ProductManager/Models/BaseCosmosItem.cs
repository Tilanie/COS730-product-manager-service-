using Newtonsoft.Json;

namespace ProductManager.Models
{
    public class BaseCosmosItem
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public virtual DataLayerType DataLayerType { get; set; }
    }
}
