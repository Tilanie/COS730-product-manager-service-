using Newtonsoft.Json;

namespace ProductManager.Models
{
    public class QueueModel
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }


        [JsonProperty(PropertyName = "Tasks")]
        public TaskModel[]? Tasks { get; set; }


        [JsonProperty(PropertyName = "Users")]
        public User[]? Users { get; set; }


        [JsonProperty(PropertyName = "State")]
        public string State { get; set; }

        public string[] TaskIds { get; set; }

        public string[] UserIds { get; set; }

        public DataLayerType DataLayerType => DataLayerType.Queue;
    }
}
